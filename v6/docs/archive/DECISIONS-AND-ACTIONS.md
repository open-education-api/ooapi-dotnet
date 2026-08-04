# Decisions and Actions

The reasoning behind this implementation's notable design choices and a summary of what it
supports - for anyone extending or evaluating this codebase who wasn't around when it was built.
For anything still outstanding, see `docs/TODO-LIST.md`. For known spec-level gaps, see the
[README](../../README.md#known-spec-ambiguities-and-gaps).

## Scope and architecture

- **Read-only generic base, explicit writes.** The shared `GenericEntityController<TEntity,TApiModel>`
  only provides `GetAll`/`GetById`. Every write action (`PUT`/`POST`/`PATCH`) is implemented
  explicitly per controller instead, with the exact request/response shape the spec defines for
  that operation. This API is not a generic writable data store - institutions are expected to
  populate data via their own integrations (syncing from a SIS/LMS, etc.), and it only exposes the
  writes the spec actually requires.
- **Per-provider migration history.** `SqlServerOEAPIDbContext`/`PostgreSqlOEAPIDbContext` each
  carry their own independent migration snapshot, so generating a migration for one provider can
  never affect the other's.
- **`OtherCodes` is a real relational owned collection**, not a JSON blob column - one dedicated
  table per owning entity, genuinely queryable rather than an opaque string.
- **The spec's `ext` field is omitted from a response entirely when there's no data**, rather than
  returned as an explicit `null`. `ext` exists purely as an anchor for institutions to attach their
  own custom data when they have something to add.
- **Every resource's own identifier is a real GUID**, matching the spec's `format: uuid`
  requirement for these fields. Human-readable values live in the separate `primaryCode` field,
  which the spec defines as a free-form business code - the two are not interchangeable.
- **`GET /`'s service metadata is generated from the application's live route table**, not
  hand-maintained, so it cannot drift from what the API actually serves. The rest of that response
  (contact/spec/documentation links, supported consumers/expands) is configuration (`Service` in
  `appsettings.json`, see the README), not a database table.
- **`GET /documents/{documentId}` is a binary file download** (`application/octet-stream`), not a
  JSON resource, per spec - the only documents-related operation this API implements. Institutions
  plug in their own storage backend via `IDocumentStorageProvider` (see the README).
- **Entity→API-model mapping is one static extension method per resource type**, in
  `src/OEAPI.Infrastructure/Data/Mapping/` (e.g.
  `OrganisationMappingExtensions.ToApiModel(this OrganisationEntity, string? consumer)`/
  `.ToEntity(this Organisation)`/`.UpdateFrom(this OrganisationEntity, Organisation)`). Every
  controller that needs to embed a foreign resource (e.g. `expand=organisation`) reuses the same
  mapping, rather than each controller carrying its own copy. Deliberately not a separate
  Application project: the mapping functions are plain functions over POCOs with no ASP.NET Core
  dependency, so a folder within the existing project structure gives the same de-duplication
  without an extra assembly boundary. A nested-listing endpoint (e.g. `Group` via
  `/course-offerings/{id}/groups`) uses the exact same mapping as that resource's own primary
  endpoint, since the spec declares the same response schema for both.
- **`BaseEntity` (`src/OEAPI.Infrastructure/Data/Entities/BaseEntity.cs`) carries the `Id`/`CreatedAt`/
  `ModifiedAt`/`IsActive` block every entity needs**, rather than each entity class repeating it. An
  unmapped `abstract class` (no `[Table]`), so EF Core treats it purely as a property source via
  ordinary C# inheritance, not table-mapping inheritance.
- **Repeated `.Include()` chains for a self-referencing entity's canonical navigation set are
  centralised in `EntityIncludeExtensions.IncludeHierarchy()`**
  (`src/OEAPI.Infrastructure/Query/Extensions/`) - one overload per entity type
  (`Organisation`/`AcademicSession`/`LearningOutcome`), covering both each controller's own
  `ApplyIncludes` and every ad-hoc `expand=`-lookup query elsewhere that needs the same navigation set.
- **Entity string columns are length-bounded** (`[StringLength]`/`[MaxLength]`) except the columns
  that deliberately store serialized JSON (multi-language objects, extension blobs, nested
  object/array data - a max length would be arbitrary and semantically wrong for these).
- **Class diagrams** (`docs/diagrams/api-model.svg`/`database-schema.svg`, rendered from the `.puml`
  sources alongside them via `docs/diagrams/render.sh`/`render.ps1`) document the public OOAPI
  resource model and the persisted EF Core schema respectively. Regenerate after any entity/API-model
  change.
- **8 top-level "list all" collections have no canonical-spec counterpart, and default to off.**
  `GET /course-offerings`, `/course-offering-associations`, `/learning-component-offerings`,
  `/learning-component-offering-associations`, `/programme-offerings`,
  `/programme-offering-associations`, `/test-component-offerings`,
  `/test-component-offering-associations` (`NonCanonicalTopLevelListRoutes`) - the canonical spec
  only ever exposes these 8 resource types nested under a parent, or by single-item `GET`, never as
  a flat top-level list. Each is a free side effect of `GenericEntityController<TEntity,TApiModel>`,
  the same base class every canonically-top-level resource controller also uses, not a
  deliberately-requested feature, so whether to expose this non-spec surface area is left to each
  deployment: `GetAll` returns `404` unless explicitly opted in
  (`Service:ExposeNonCanonicalListEndpoints`, `false` by default), and
  `NonCanonicalListEndpointDocumentTransformer` removes these 8 paths from the self-generated OpenAPI
  document while disabled, so a `404`-by-default endpoint isn't advertised as documented API surface.
  Every canonical top-level resource's own list endpoint is unaffected either way. Path-parameter
  names on every single-item `GET` this codebase exposes match the canonical spec's own semantic name
  (e.g. `{organisationId}`, not a generic `{id}`).

## Extensible enums

OOAPI's spec defines around 30 `x-ooapi-extensible-enum` fields: a fixed set of known values, *plus*
any value prefixed `x-` for institution-specific custom values (e.g. `organisationType`,
`offeringState`, `codeType`). This shape has no direct C# equivalent:

- **A plain C# `enum` doesn't work.** `[JsonConverter(typeof(JsonStringEnumConverter))]`
  deserialization only accepts the enum's declared members - a real, spec-legal client value like
  `x-mycustomvalue` fails deserialization outright (400, and for the wrong reason - a parse failure
  before model binding even completes, not a validation error).
- **A plain `string`/`string[]` with no validation also doesn't work** - it silently accepts any
  value at all, known or not, so nothing enforces even the loose "known value or `x-`-prefixed"
  contract.

**The solution used throughout this codebase**: type the field as a plain `string`/`string[]`, and
attach `[ExtensibleEnum("schemaName")]`
(`src/OEAPI.Core/Models/ApiModels/Validation/ExtensibleEnumAttribute.cs`) - a
`System.ComponentModel.DataAnnotations.ValidationAttribute` that checks "one of the schema's known
values (case-insensitive), or `x`-prefixed" at request-validation time. `[ApiController]`'s built-in
automatic model validation runs this on every `[FromBody]`-bound property with no controller code
needed, returning a 400 `ValidationProblemDetails` on failure. The known-value lists themselves live
in one central registry, `src/OEAPI.Core/Models/ApiModels/Validation/ExtensibleEnumValues.cs` - a
`Dictionary<string, string[]>` keyed by the spec's own schema name (e.g. `"offeringState"`), so a
schema backing the same-shaped field on multiple otherwise-unrelated classes (e.g. `offeringState`
backs `State` on all 4 offering types) only needs its value list maintained in one place, and the
`[ExtensibleEnum("...")]` usage itself doubles as a direct pointer back to the spec schema name. An
unregistered schema name throws `InvalidOperationException` at validation time - a fail-fast guard
against a typo or a forgotten registration, rather than silently accepting everything.

**To add a new extensible-enum field**: confirm the value list against the spec's
`source/enumerations/{schemaName}.yaml` (the `x-ooapi-extensible-enum` list), add an entry to
`ExtensibleEnumValues.Values` if that schema isn't already registered, then type the API model
property as `string`/`string?`/`string[]?` (matching the schema's cardinality) with
`[ExtensibleEnum("schemaName")]`. No enum type, no custom `JsonConverter`, no entity-layer change
needed - every entity column backing one of these fields is already a plain string column.

Every `x-ooapi-extensible-enum` field the spec declares is modelled and validated this way - no C#
`enum` type remains anywhere in the API-model layer for a field the spec marks extensible (the one
genuinely closed/non-extensible enum in this codebase, `filterPresence`, is the `filter_query` DSL's
own internal presence operator, not a domain field, and correctly stays a real `enum`).

## Supported query features

Every list endpoint supports:

- **`fields`** - the spec's nested-parens field-selection syntax (`fields=(id,title,programme(code))`).
  A selection only ever narrows *optional* fields; every field the resource's spec schema marks
  `required` (including nested `expand`ed objects' own required fields) is always included regardless
  of what's requested. Required-field sets are generated from the canonical spec itself
  (`scripts/extract-spec-manifest.py`'s `build_field_requirements` step, embedded as
  `src/OEAPI.Infrastructure/Query/Fields/field-requirements.json`), not hand-duplicated.
- **`filter_query`** - the full DSL: all 12 spec operators (including `in`/`not_in` against
  numeric-typed fields, not just string ones), top-level `AND` plus `__or` blocks, including filtering
  through real cross-entity navigation paths (e.g. `organisation.primaryCode`, and flattened
  relation-id fields like `filter_query[organisationId][in]=...`, resolved via each entity's own
  `[Index(..., IsUnique = true)]`-marked external id property). Internal-only bookkeeping fields
  (`id`, `consumerKey`, `createdAt`, `modifiedAt`, `isActive`, and raw JSON-blob storage columns) are
  excluded from resolution (`FilterQueryTranslator.InternalOnlyPropertyNames`), since none of these
  are part of any API model's contract, and `consumerKey` in particular is the same field the
  `consumer` parameter above partitions on.
- **`consumer`** - filters results to a specific consumer, and returns that consumer's
  extension data when requested. Two consumers with published schemas are implemented with typed
  data (RIO, eduXchange); any other consumer key falls back to a generic passthrough.
- **`expand`** - returns full nested objects for relationship fields instead of just their
  identifiers. Every relationship in the current spec is two separate properties (e.g.
  `Organisation.parentId`/`.parent`) - the `Id` field is always present when the relationship
  exists, and the expanded field is only populated (otherwise `null`) when the client requests it.
  Covers every relationship shape this implementation exposes: single-valued, array-valued, and
  multiple targets resolved together in one request. Two spec-authoring gaps affecting `expand` are
  filed upstream - see the README's "Known spec ambiguities and gaps".

  Black-box conformance testing against this implementation can only meaningfully cover resources
  that have an exact-match, canonical, flat top-level collection endpoint declared in the spec (the 8
  non-canonical `-offerings`/`-associations` resources and nested-only resources like
  `learning-components` are permanently out of reach for that style of check, regardless of demo
  data, because the canonical spec never declares a flat collection for them) - this implementation's
  own `expand=` behaviour is verified independently, via its own integration test suite, for exactly
  this reason.

`GET /persons` additionally supports the spec's `q` (case-insensitive search across name/email
fields) and `affiliations` filters.

- **`returnTimelineOverrides`** - `GET /courses/{id}`/`GET /programmes/{id}` only (the spec declares
  it on no other operation), returns historical/future alternate snapshots of the resource as
  `timelineOverrides: [{validFrom, validTo?, course|programme: {...}}]`, each entry a full,
  independent copy of every field `Course`/`Programme` itself currently exposes - not shared with the
  parent resource or with any other override entry. Absent by default; also omitted (never an empty
  array) when explicitly requested but nothing is seeded, matching this codebase's `ext`-field
  precedent of omitting rather than emitting an empty/null marker. The caller's own `expand=`
  composes into every override entry the same way it applies to the parent resource (e.g.
  `?returnTimelineOverrides=true&expand=organisation` expands `organisation` both at the top level
  and inside each `timelineOverrides[].course.organisation`). Wired via a
  `protected virtual ApplyTimelineOverridesAsync` hook on `GenericEntityController` (default no-op),
  read from the raw query string rather than a formal `GetById` parameter, since only these two
  resources support it. `Programme`'s override `children` uses its own join table
  (`ProgrammeTimelineOverrideChildren`) rather than the live `ParentEntityId` hierarchy column, which
  belongs to the child row and reflects only its one current, live parent.

## Writes

All spec-required writes are implemented:

- 11 `PUT` upsert endpoints (`200` replacing an existing resource, `201` creating one at the
  client-supplied id). These return no response body, matching the spec's own declared shape for
  these operations.
- `POST /persons`, using a narrower request shape than the full `Person` response model (no
  `personId` - the server generates it), and responding with the spec's `PersonId` + `PostResponse`
  shape (`{personId, message, redirect?}`), not the full `Person` resource. Both `POST /persons` and
  `PUT /persons/{personId}` reject a body where `surname`, `givenName`, and `preferredName` are all
  absent, since the response schema's own `anyOf` requires at least one of them.
- `PATCH` on all 9 spec-required operations: the 4 "-offerings" resources take a full
  representation body; the 4 "-associations" resources use genuine JSON Merge Patch (RFC 7396,
  `application/merge-patch+json`, a narrow `{remoteState?, result?}` body).
- The two `external/me` `POST` endpoints (a person enrolling themselves), which resolve the caller's
  identity via the pluggable `ICurrentPersonProvider` seam and return an `AssociationWriteResponse`.
  A unique-constraint violation on any write, on either database provider, returns a clean `409
  Conflict` - correct, expected REST behaviour even though the spec doesn't document `409` as a
  possible response for any write operation.
- A required key whose own value schema allows an explicit `null` (e.g. `remoteState` on both
  `external/me` `POST` endpoints) accepts that explicit `null` the same as any other valid value,
  rather than rejecting it as "field is required". An omitted key is accepted the same way, since
  plain C# property binding can't distinguish an explicitly-`null` key from a wholly absent one.
- Omitting an optional nested value-object (e.g. `primaryCode` on the association/`Person`/`Membership`
  types where it's genuinely optional per the spec) is accepted, not validated against a phantom
  default instance - the server only validates a nested object's own fields when the client actually
  provided that object.
- **Every API model's write-reachable string field carries a `[StringLength]`/`[RegularExpression]`
  matching the persistence layer's own constraint on that column**, so an over-length or
  pattern-violating value gets a clean `400` at the API boundary instead of reaching the database and
  crashing with an unhandled `SqlException`/`PostgresException`. Covers all 13 write-reachable
  entities plus `Cost.amount`/`vatAmount`/`amountWithoutVat`'s pattern. Since the canonical spec
  declares no `maxLength` for these fields (they're implementation-specific storage limits, not spec
  requirements), a schema-fuzzing tool that only knows the bare spec may flag some of these `400`s as
  "rejected a schema-valid request" - an accepted trade-off, since a clean `400` for data that
  exceeds real storage capacity is strictly better than an unhandled `500`.
- **`ApiModelStringLengthSyncTests`** (`tests/OEAPI.Infrastructure.Tests/Data/Entities/`) guards
  against the API-model/entity `[StringLength]` pair above drifting apart over time - reflection-based
  rather than a hand-maintained field list, so a new field added later is covered automatically. A
  small, explicit override dictionary handles the few properties where the entity-side name genuinely
  differs (`Organisation.ShortName`/`.Link`/`.Logo` -> `OrganisationEntity.Abbreviation`/`.WebsiteUrl`/
  `.LogoUrl`).
- **A custom `[RegexPattern]` attribute** (`OEAPI.Core.Models.ApiModels.Validation`) exists alongside
  the built-in `[RegularExpression]` because the built-in one silently no-ops on a `string[]` property
  (falls back to `.ToString()` on the array itself) - needed for `teachingLanguages` and similar
  array-typed pattern-constrained fields. Its `RegexPatterns.Values["language"]` entry is a corrected
  RFC 5646-based pattern, not the spec's own literal one (see the README's known-gaps list, #698).

## Response correctness

- **`TestComponentOfferingAssociation.attempt` (singular) has no backing property anywhere in the
  entity or API model.** The spec marks it `deprecated: true`, superseded by
  `initialAttemptOnAssociation`, which this implementation fully supports and seeds - deliberately
  not implemented.
- **`Identifier`-typed fields** (relationship id fields like `parentId`, distinct from
  `IdentifierEntry`/`primaryCode`/`otherCodes[]`) serialize as a plain UUID string, matching the
  spec's `Identifier.yaml` schema exactly - via a class-level `[JsonConverter(typeof(IdentifierJsonConverter))]`
  on `Identifier`, so it applies uniformly to every response, request body, and `fields=`-pruned path
  without needing separate wiring.
- **Error responses** are served as `application/problem+json` (a dedicated
  `ProblemDetailsOutputFormatter`, registered ahead of the default JSON formatter) and always include
  the RFC 7807 `type` field, backfilled from the status code via `ControllerBase.Problem(...)`/
  `ProblemDetailsFactory` everywhere in the codebase.
- **Optional array-typed fields** whose spec schema doesn't allow `null` (no `"null"` in the JSON
  Schema `type` array) are serialized as `[]` when empty, never `null` - array fields whose schema
  does allow `null` (most `childIds`-style relationship arrays) correctly still return `null`, per
  their own schema.
- **Pagination never crashes regardless of `pageNumber`/`pageSize`**, on every paginated endpoint
  including nested/child-collection ones, not just top-level list endpoints. All share one
  implementation (`GenericEntityController.ComputePageOffset`), which clamps the computed offset to
  `int.MaxValue` on overflow rather than ever passing a negative value to the database.
- **`GET /`'s `supportedExpands`** lists, per operation, exactly the `expand` values that operation's
  own canonical-spec-declared `expand` enum allows - generated from the spec directly rather than
  hand-maintained (`src/OEAPI.API/appsettings.json`'s `Service:SupportedExpands`, cross-checked
  against the spec's own per-operation enums).
- **`Accept`-header OEAPI/consumer version negotiation** (`VersionNegotiationMiddleware`, registered
  ahead of `AuthenticationMiddleware` - version compatibility is checked before identity) rejects a
  request that explicitly asks for an incompatible OEAPI major version, or an unknown
  consumer/consumer-version, with `406` + a `ProblemVersionNotAcceptable` body
  (`requestedVersion`/`supportedVersions`/`consumer`). A request with no OEAPI version in `Accept` at
  all (missing header, `*/*`, plain `application/json`, ...) is a deliberate, documented exception to
  the spec's literal "closed versioning" wording: it's treated as no preference stated and always
  served, never rejected. `Service:SupportedOeapiVersions` supplies which versions this deployment
  can serve; resolution allows any minor-version fallback within a matching major, never a
  major-version fallback. A successful, plain-JSON response echoes the resolved version(s) back via
  `Content-Type` (`application/vnd.oeapi+json;version=X.Y[;consumer=...;consumer-version=...]`)
  **only when the client's `Accept` explicitly named a version**
  (`VersionNegotiationResult.WasExplicitlyRequested`) - via `Response.OnStarting(...)`, since an early
  header assignment gets silently overwritten by ASP.NET Core's own content-type selection. A generic
  request (no version named) is passed through untouched, keeping plain `application/json` - this
  deployment's own self-generated `/openapi/v1.json` never declares the versioned media type, so
  echoing it unconditionally for every request would make the reference implementation inconsistent
  with its own schema. Deliberately scoped to plain `application/json` responses only -
  `application/problem+json` error responses and non-JSON content (e.g.
  `GET /documents/{documentId}`'s binary download) are never relabelled.

## Testing approach

- Integration tests run against real, disposable SQL Server and PostgreSQL containers
  (Testcontainers), not the shared local dev databases and not an in-memory/SQLite fake -
  provider-specific behaviour (e.g. default string-comparison collation) can differ from what an
  in-memory substitute would show.
- The conformance test suite is generated from the spec file itself (`scripts/extract-spec-manifest.py`),
  covering every `GET`/`PUT`/`POST`/`PATCH` operation the spec defines, plus targeted tests for
  upsert/merge-patch semantics, `expand`/`fields`/`filter_query` correctness, and the `external/me`
  auth seam. Spec-first rather than implementation-first, so the tests can't simply codify whatever
  the code currently happens to do.

## Demo data

- **`DemoDataSeeder` (opt-in via `Database:SeedDemoData`) seeds a genuinely connected dataset, not
  one isolated row per resource.** Every resource the API exposes has at least two rows, with real
  relationships between them: an organisation hierarchy (root + child, so `parent`/`children`/`root`
  expand all return something), an academic year with one of its semesters, a learning outcome
  parent/child pair, course/programme coordinators and instructors, group-to-offering links across
  all 4 offering types, room assignments on offerings, a building with a real linked address, and
  `otherCodes` on most main resources. Built so a spec-conformance prober that discovers data by
  walking the graph (list an endpoint, follow a nested/`expand` link, list the next one) finds real
  data wherever it looks.

## Documentation

- Interactive API docs (Scalar) consume this implementation's own generated OpenAPI document
  directly, with a concrete response schema for every operation (not just status codes).
- The shared `ProblemDetails` error schema carries a realistic example (via an
  `IOpenApiSchemaTransformer` registered in `Program.cs`), so error responses show a real-looking
  instance in Scalar instead of every field defaulting to `null`.
- The generated OpenAPI document's `info`/`license`/`externalDocs` fields identify which OEAPI
  version this deployment implements and link to the public specification/documentation (via
  `ServiceInfoDocumentTransformer`, an `IOpenApiDocumentTransformer` reading the same
  `ServiceConfiguration` that backs `GET /`). `info.license` states EUPL-1.2, matching this
  repository's own `LICENSE.md` and mirroring the licence the OEAPI specification itself declares.
- **`RegexPattern`/`ExtensibleEnum` constraints are reflected into the generated schema**, not just
  enforced at request time, via `ValidationAttributeSchemaTransformer` (an `IOpenApiSchemaTransformer`
  registered in `Program.cs`). It runs once per property, reads the CLR property's attributes off
  `JsonPropertyInfo.AttributeProvider`, and sets `Schema.Pattern` for `[RegexPattern]` or an
  `anyOf: [{enum: [...]}, {pattern: "^x-"}]` for `[ExtensibleEnum]` (an `anyOf` rather than a plain
  `enum`, since a plain `enum` can't also allow the spec's `x-`-prefixed custom-value escape hatch).
  For a string-array property, the constraint is written onto the array's `items` schema rather than
  the array schema itself - note `JsonSchemaType` is a `[Flags]` enum, so a nullable array property's
  `Type` is `Null | Array`, not `Array` alone; comparing for equality against `JsonSchemaType.Array`
  never matches and has to use `HasFlag` instead.

## Security

- **`AuthenticationMiddleware` never writes raw exception messages to the response.** A registered
  `IJwtAuthenticationService`/`IApiKeyAuthenticationService`/`IAuthenticationService` implementation
  that throws (e.g. a wrapped database failure) has its exception logged server-side only; the
  client-facing 401 body is a fixed, generic message. This matters because institutions are expected
  to plug a real implementation into this exact scaffolding as their primary path to production.
- **`filter_query` cannot be used to probe internal-only entity fields.** Its reflection-based field
  resolution (`FilterQueryTranslator.ResolvePath`) excludes bookkeeping properties that aren't part
  of any API model's contract (see the `filter_query` entry above) - most importantly `consumerKey`,
  the same field `?consumer=` partitions data on, which would otherwise have been enumerable via
  `filter_query` as a boolean-oracle side channel.
