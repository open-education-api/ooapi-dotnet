#!/usr/bin/env python3
"""
Extracts a compact, machine-readable manifest of every operation in the OOAPI/OEAPI v6 spec, published
at https://github.com/open-education-api/specification, for use by the generic spec-driven conformance
test (`tests/OEAPI.API.Tests/SpecConformance/`). The source spec is ~640KB/17k lines - too large to
re-read into an LLM context every session - so this script is run once (and re-run only if the spec
changes) to produce a small manifest that both the C# test and a human session can read cheaply.

Also emits a second, much smaller file - `field-requirements.json` - resolving each schema's real
`required` field set (merging `allOf`/`$ref` composition, e.g. `Course` = `CourseId` + `CourseProperties`
+ an inline object) plus a property-name -> nested-schema-name map for object-valued properties (e.g.
`Organisation.parent` -> `Organisation`). This is embedded into `src/OEAPI.Infrastructure` (a runtime
project, not just the test assembly) so `FieldPruner` can look up "what must always survive a `fields=`
selection" from the spec itself rather than a hand-maintained/heuristic guess - see
`src/OEAPI.Infrastructure/Query/Fields/FieldPruner.cs`.

Usage: python scripts/extract-spec-manifest.py [--spec PATH_OR_URL]
    --spec defaults to the same tagged release this project's own `Service:Specification` config
    points at. Pass a local file path or a different URL to regenerate against another version (e.g.
    a locally-bundled copy of a newer, not-yet-tagged `source/` tree from the specification repo).
Output: tests/OEAPI.API.Tests/SpecConformance/spec-manifest.json
        src/OEAPI.Infrastructure/Query/Fields/field-requirements.json
"""
import argparse
import json
import pathlib
import urllib.request

REPO_ROOT = pathlib.Path(__file__).resolve().parent.parent
DEFAULT_SPEC_URL = (
    "https://raw.githubusercontent.com/open-education-api/specification/release/6.0/oeapi.json"
)
OUTPUT_PATH = REPO_ROOT / "tests" / "OEAPI.API.Tests" / "SpecConformance" / "spec-manifest.json"
FIELD_REQUIREMENTS_OUTPUT_PATH = (
    REPO_ROOT / "src" / "OEAPI.Infrastructure" / "Query" / "Fields" / "field-requirements.json"
)


def load_spec(spec_location: str) -> dict:
    """Reads `spec_location` as a URL if it looks like one, otherwise as a local file path -
    matching the `--spec` convention already used by the sibling conformance testing tool."""
    if spec_location.startswith(("http://", "https://")):
        with urllib.request.urlopen(spec_location) as response:  # noqa: S310 - trusted, user-supplied
            return json.loads(response.read().decode("utf-8"))
    return json.loads(pathlib.Path(spec_location).read_text(encoding="utf-8"))


METHODS = ["get", "post", "put", "patch", "delete"]


def extract_path_params(operation: dict) -> list[dict]:
    params = []
    for param in operation.get("parameters", []):
        if isinstance(param, dict) and param.get("in") == "path":
            schema = param.get("schema", {})
            params.append({
                "name": param["name"],
                "format": schema.get("format"),
            })
    return params


def extract_request_body(operation: dict) -> dict | None:
    body = operation.get("requestBody")
    if not body:
        return None
    content = body.get("content", {})
    if not content:
        return None
    content_type = next(iter(content.keys()))
    return {
        "contentType": content_type,
        "required": bool(body.get("required", False)),
    }


def extract_success_codes(operation: dict) -> list[int]:
    codes = []
    for code in operation.get("responses", {}):
        if code.isdigit() and int(code) < 300:
            codes.append(int(code))
    return sorted(codes)


def extract_response_schema(operation: dict) -> dict | None:
    """Returns the raw JSON Schema (OpenAPI 3.1 schemas *are* JSON Schema draft 2020-12 - this spec
    uses no OpenAPI-3.0-isms like `nullable: true`, confirmed by inspection) for the operation's
    success response body, for real schema-conformance validation (not just status-code checks).
    Only `application/json` responses are extracted - `GET /documents/{documentId}` is a binary
    file download with no JSON schema to validate, and is correctly skipped here.
    """
    responses = operation.get("responses", {})
    for code in sorted(c for c in responses if c.isdigit() and int(c) < 300):
        schema = responses[code].get("content", {}).get("application/json", {}).get("schema")
        if schema:
            return schema
    return None


def is_paged_response(operation: dict) -> bool:
    """True if the 200 response schema is the spec's Pagination shape (allOf[...,
    {required: [items], ...}]) - i.e. a genuine paginated list, not a single resource. Determined
    from the actual response schema, not the path shape: nested collection endpoints (e.g.
    /organisations/{id}/course-offerings) are also paginated but conditionally 404 if the parent
    doesn't exist, so path shape alone (no trailing {param}) is not a reliable signal - see the
    isListGet vs hasPathParams split this feeds into.
    """
    response = operation.get("responses", {}).get("200")
    if not response:
        return False
    schema = response.get("content", {}).get("application/json", {}).get("schema", {})
    for member in schema.get("allOf", []):
        if member.get("required") == ["items"]:
            return True
    return False


def _ref_name(ref: str) -> str:
    return ref.rsplit("/", 1)[-1]


def resolve_required_fields(schema: dict, schemas: dict, visited: set[str]) -> set[str]:
    """Merges `required` across a schema's own declaration and any `allOf`-composed members
    (resolving `$ref`s, recursively, guarding against cycles via `visited`). Anonymous inline
    `allOf` members (e.g. `Course`'s inline `{validFrom, validTo}` object) contribute their own
    `required` too, though in practice the spec never marks fields required that way.
    """
    required: set[str] = set(schema.get("required", []))

    if "$ref" in schema:
        name = _ref_name(schema["$ref"])
        if name not in visited:
            required |= resolve_required_fields(schemas[name], schemas, visited | {name})

    for member in schema.get("allOf", []):
        required |= resolve_required_fields(member, schemas, visited)

    return required


def resolve_property_schema_name(property_schema: dict) -> str | None:
    """Returns the named component schema a property's value resolves to, if any - e.g.
    `Organisation.parent` (`type: [object, null], allOf: [{$ref: .../Organisation}]`) -> `Organisation`,
    `Organisation.children` (`type: [array, null], items: {allOf: [{$ref: .../Organisation}]}}`) ->
    `Organisation`. Returns `None` for scalar fields or inline (non-`$ref`) object shapes.
    """
    if "$ref" in property_schema:
        return _ref_name(property_schema["$ref"])

    for member in property_schema.get("allOf", []):
        if "$ref" in member:
            return _ref_name(member["$ref"])

    for member in property_schema.get("oneOf", []):
        if "$ref" in member:
            return _ref_name(member["$ref"])

    if "items" in property_schema:
        return resolve_property_schema_name(property_schema["items"])

    return None


def build_field_requirements(schemas: dict) -> dict:
    result = {}
    for name, schema in schemas.items():
        required = sorted(resolve_required_fields(schema, schemas, {name}))
        properties = schema.get("properties", {})
        if not properties:
            for member in schema.get("allOf", []):
                if "$ref" in member:
                    properties = {**schemas[_ref_name(member["$ref"])].get("properties", {}), **properties}
                else:
                    properties = {**member.get("properties", {}), **properties}

        nested = {
            prop_name: schema_name
            for prop_name, prop_schema in properties.items()
            if (schema_name := resolve_property_schema_name(prop_schema)) is not None
        }
        result[name] = {"required": required, "properties": nested}

    return result


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--spec", default=DEFAULT_SPEC_URL, help="Local file path or URL to the bundled OpenAPI JSON document")
    args = parser.parse_args()

    spec = load_spec(args.spec)
    operations = []

    for path, path_item in spec["paths"].items():
        for method in METHODS:
            if method not in path_item:
                continue
            operation = path_item[method]
            operations.append({
                "path": path,
                "method": method.upper(),
                "operationId": operation.get("operationId"),
                "tags": operation.get("tags", []),
                "pathParams": extract_path_params(operation),
                "requestBody": extract_request_body(operation),
                "successStatusCodes": extract_success_codes(operation),
                "isPagedResponse": is_paged_response(operation),
                # Only a *top-level* paginated GET (zero path params - nothing that could itself be
                # "not found") is guaranteed to return 200 against an empty database. A nested
                # collection (e.g. /organisations/{id}/course-offerings) is also paginated but
                # legitimately 404s if the parent id doesn't exist.
                "isTopLevelListGet": method == "get" and is_paged_response(operation) and not extract_path_params(operation),
                "responseSchema": extract_response_schema(operation),
            })

    manifest = {
        "sourceSpec": args.spec,
        "operationCount": len(operations),
        "operations": operations,
        # Embedded once, shared by every operation's responseSchema via "$ref":
        # "#/components/schemas/X" - resolved by wrapping {"components": {"schemas":
        # componentSchemas}, ...responseSchema} as the JSON Schema document being validated
        # against, so "#/..." fragment refs resolve as ordinary same-document JSON Pointers with
        # no special base-URI setup needed.
        "componentSchemas": spec["components"]["schemas"],
    }

    OUTPUT_PATH.parent.mkdir(parents=True, exist_ok=True)
    OUTPUT_PATH.write_text(json.dumps(manifest, indent=2) + "\n", encoding="utf-8")
    print(f"Wrote {len(operations)} operations to {OUTPUT_PATH}")

    field_requirements = {
        "sourceSpec": args.spec,
        "schemas": build_field_requirements(spec["components"]["schemas"]),
    }
    FIELD_REQUIREMENTS_OUTPUT_PATH.parent.mkdir(parents=True, exist_ok=True)
    FIELD_REQUIREMENTS_OUTPUT_PATH.write_text(
        json.dumps(field_requirements, indent=2) + "\n", encoding="utf-8"
    )
    print(f"Wrote {len(field_requirements['schemas'])} schemas to {FIELD_REQUIREMENTS_OUTPUT_PATH}")


if __name__ == "__main__":
    main()
