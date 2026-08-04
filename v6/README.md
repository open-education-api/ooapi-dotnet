# OOAPI v6 .NET Reference Implementation

A .NET 10 / ASP.NET Core reference implementation of the [Open Education API (OOAPI) v6
specification](https://oeapi.eu/) — organisations, persons, courses, programmes,
offerings, associations, groups, and the rest of the spec's resource model, backed by EF Core with
a choice of SQL Server or PostgreSQL. The specification itself (schemas, paths, enumerations, and
its own issue tracker) lives at
[open-education-api/specification](https://github.com/open-education-api/specification) on GitHub
- this repository implements it but doesn't vendor a copy of it.

This document is for institutions and developers **deploying or extending** this implementation.
For the codebase's own development history and internal conventions, see `docs/`.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A database server: SQL Server (including LocalDB) or PostgreSQL

## Quick start

```bash
git clone <this-repo-url>
cd <cloned-directory>
dotnet build OEAPI.slnx
dotnet run --project src/OEAPI.API
```

By default this connects to a local SQL Server instance (see [Configuration](#configuration)
below) and applies pending migrations automatically in the `Development` environment. Once
running:

- Interactive API docs (Scalar): `http://localhost:5000/scalar/v1` (Development only)
- Raw OpenAPI document: `http://localhost:5000/openapi/v1.json` (Development only)
- Health check: `http://localhost:5000/health`

Set `ASPNETCORE_ENVIRONMENT=Development` if it isn't already (the `http` launch profile in
`src/OEAPI.API/Properties/launchSettings.json` does this for you, as does VS Code's included
`.vscode/launch.json` — press F5/Run there and it builds, launches, and opens Scalar automatically).

To try the API out immediately with some sample data instead of an empty database, set
`Database:SeedDemoData` to `true` (see below) — this seeds one interconnected record of every
resource type (an organisation, persons, courses, offerings, associations, a downloadable
document, etc.), all tagged with a `DEMO-*` primary code so it's unambiguously recognizable as
sample data.

## Configuration

All configuration lives under `Database`, `Authentication`, and `Service` in `appsettings.json` /
`appsettings.Development.json`, or the equivalent environment variables
(`Database__Provider`, `Database__ConnectionStrings__SqlServer`, etc. — .NET's standard
`__`-as-`:` convention). See `DATABASE_CONFIGURATION.md` for the full database configuration guide
(switching providers, connection string examples, migrations, production hardening).

```json
{
  "Database": {
    "Provider": "SqlServer",
    "ConnectionStrings": {
      "SqlServer": "Server=(localdb)\\YourInstance;Database=OEAPIDatabase;Trusted_Connection=True;MultipleActiveResultSets=true",
      "PostgreSQL": "Host=localhost;Port=5432;Database=OEAPIDatabase;Username=...;Password=..."
    },
    "SeedDemoData": false
  },
  "Authentication": {
    "Providers": { "JwtEnabled": false, "ApiKeyEnabled": false, "CustomEnabled": false },
    "RequireAuthentication": false
  },
  "Service": {
    "ContactEmail": "admin@your-institution.example",
    "Specification": "https://raw.githubusercontent.com/open-education-api/specification/release/6.0/oeapi.yaml",
    "Documentation": "https://oeapi.eu/",
    "SupportedConsumers": [
      { "ConsumerKey": "your-consumer-key", "Version": "1.0.0" }
    ],
    "SupportedExpands": [
      { "ExpandableObjects": [ "parent", "children" ], "Path": "/academic-sessions" }
    ]
  }
}
```

- `Database:Provider` — `"SqlServer"` or `"PostgreSQL"`. Only the connection string for the
  selected provider needs to be set.
- `Database:SeedDemoData` — see [Quick start](#quick-start) above. Defaults to `false`;
  `appsettings.Development.json` overrides it to `true` for local convenience. Leave it `false` in
  production unless you specifically want sample data.
- `Authentication:RequireAuthentication` — **`false` by default**, everywhere, including
  production config. This implementation ships with no authentication enforced; see
  [Authentication](#authentication) below before deploying anywhere that matters.
- `Service` — the response for `GET /`, the spec's service-metadata endpoint (contact details, the
  spec/documentation URLs this deployment implements, and which consumers/`expand`s it supports).
  Edit this section directly to describe your own deployment - there is no admin UI or write
  endpoint for it, since the spec itself defines no `POST`/`PUT` for this resource.
  `Service:SupportedOperations` is **not** configurable here: it's generated automatically from the
  application's own live route table (see `ServiceController`), so it always matches what this
  deployment actually serves and can't drift out of sync the way a hand-maintained list would.

## Authentication

Authentication is deliberately left as an extension point rather than a fixed implementation —
every institution has its own IAM. Three provider slots exist, toggled independently under
`Authentication:Providers`:

| Config flag     | Interface to implement                                   | Purpose                                    |
|-----------------|----------------------------------------------------------|--------------------------------------------|
| `JwtEnabled`    | `IJwtAuthenticationService` (`OEAPI.Core.Interfaces`)    | Bearer token validation                    |
| `ApiKeyEnabled` | `IApiKeyAuthenticationService` (`OEAPI.Core.Interfaces`) | Header-based API key validation            |
| `CustomEnabled` | `IAuthenticationService` (`OEAPI.Core.Interfaces`)       | Anything else (mTLS, session cookies, ...) |

None of these have a default implementation registered — enabling a flag without registering the
corresponding service is a no-op (the middleware checks for a registered service and skips the
provider if none is found). Register your implementation in `Program.cs`'s
`ConfigureAuthenticationServices`, e.g.:

```csharp
builder.Services.AddScoped<IJwtAuthenticationService, MyJwtAuthenticationService>();
```

With `RequireAuthentication: false` (the default), unauthenticated requests are simply served
without an authentication context. Set it to `true` once you've registered a real provider to
enforce authentication on every endpoint (`[AllowAnonymous]`-marked ones excepted).

## Other pluggable extension points

Two more pieces of behaviour are deliberately deployment-specific, each behind its own interface
in `OEAPI.Core.Interfaces` with a working default registered in `Program.cs`:

- **`ICurrentPersonProvider`** — resolves which `personId` the current authenticated caller
  corresponds to, used by `GET /persons/me` and the `external/me` endpoints. The spec requires this
  to come from "a well known endpoint" (e.g. an OIDC UserInfo endpoint) rather than defining a
  concrete mechanism — this is exactly that seam. Default: `DefaultCurrentPersonProvider`.
- **`IDocumentStorageProvider`** — resolves the raw bytes for `GET /documents/{documentId}` (a
  binary file download, not JSON). Institutions with an existing document store or object storage
  (S3, Azure Blob, a DMS, ...) should replace the default with their own implementation. Default:
  `DatabaseDocumentStorageProvider`, which reads a `byte[]` column from this project's own
  database.

Replace either by changing its registration in `Program.cs`:

```csharp
builder.Services.AddScoped<IDocumentStorageProvider, MyObjectStorageDocumentProvider>();
```

## Project structure

```text
src/
  OEAPI.API/             ASP.NET Core Web API - controllers, Program.cs, configuration, auth middleware
  OEAPI.Core/             API models (spec-shaped DTOs), shared interfaces, enums
  OEAPI.Infrastructure/   EF Core entities, DbContext (SQL Server/PostgreSQL variants), migrations,
                          query/filtering engine, demo data seeder
tests/
  OEAPI.API.Tests/            Integration tests (Testcontainers-backed real SQL Server/PostgreSQL)
  OEAPI.Core.Tests/           Unit tests
  OEAPI.Infrastructure.Tests/ Unit tests (query/filter parsing, consumer resolution, etc.)
```

## Running tests

```bash
dotnet test OEAPI.slnx
```

Integration tests spin up real, disposable SQL Server and PostgreSQL containers via
Testcontainers — Docker must be running.

## Known spec ambiguities and gaps

A handful of spec-level problems are filed upstream against
[open-education-api/specification](https://github.com/open-education-api/specification), rather
than worked around silently in this codebase:

- [**#681**](https://github.com/open-education-api/specification/issues/681) -
  the versioning documentation's own `Content-Type` contract is inconsistent for explicit-version
  requests. This implementation echoes the exact requested version back
  (`VersionNegotiationMiddleware`) when a client's `Accept` header explicitly names one, but its
  self-generated OpenAPI document still declares every response as plain `application/json` for that
  case - accepted rather than fixed, pending how #681 resolves.
- [**#687**](https://github.com/open-education-api/specification/issues/687)/[**#601**](https://github.com/open-education-api/specification/issues/601) -
  nullable relationship fields (`parentId`, `parent`, and around 60 similar fields spec-wide) don't
  correctly declare `null` as an allowed value in their JSON Schema. This implementation returns
  `null` for a missing relationship regardless, on both the identifier and expanded-object field; its
  own schema-conformance suite
  (`tests/OEAPI.API.Tests/SpecConformance/SchemaConformanceTests.cs`) is skipped for these fields
  pending a spec fix.
- [**#694**](https://github.com/open-education-api/specification/issues/694) -
  `ProgrammeOfferingAssociation`'s `personId`/`person` are `readOnly: true`, but the schema's `anyOf`
  requires one of them on every `PUT` - no schema-valid request body exists. This implementation
  never required either field, so no change was needed.
- [**#695**](https://github.com/open-education-api/specification/issues/695) -
  no write operation declares `409` as a possible response, but a genuine unique-constraint conflict
  (e.g. a duplicate `primaryCode`) returns one anyway - correct REST behaviour the spec just doesn't
  document.
- [**#696**](https://github.com/open-education-api/specification/issues/696) -
  the `expandableObjects` vocabulary has no plural `parents`, even though
  `/learning-outcomes/{id}`'s `expand` enum requires it.
- [**#697**](https://github.com/open-education-api/specification/issues/697) -
  `Service.supportedOperations[].path`/`supportedExpands[].path` declare `format: uri-reference`,
  which doesn't permit the `{parameterName}` placeholder syntax the field actually needs to report.
  `format: uri-template` (RFC 6570) is correct.
- [**#698**](https://github.com/open-education-api/specification/issues/698) -
  `Language.yaml`'s `pattern` doesn't correctly implement RFC 5646 and rejects the schema's own
  documented examples (`zh-Hant-TW`, `nl-sgn-NL`, `nl-s-NL`). **This implementation deviates from the
  literal spec**: `RegexPatterns.Values["language"]`
  (`src/OEAPI.Core/Models/ApiModels/Validation/RegexPatterns.cs`) uses a corrected pattern built
  directly from RFC 5646 §2.1's grammar instead.
- [**#699**](https://github.com/open-education-api/specification/issues/699) -
  the GitHub documentation site's `expanding-responses.md` page describes a different,
  no-longer-current `expand` convention and references a schema (`Association.yaml`) that doesn't
  exist in the current spec. This implementation follows the current spec's actual convention, not
  the documented one.
- [**#700**](https://github.com/open-education-api/specification/issues/700) -
  `ProgrammeOfferingInstance.yaml`'s `expand` enum declares 5 targets that don't correspond to any
  real relationship on `ProgrammeOffering`. Same defect also affects `LearningOutcomeInstance.yaml`,
  which declares `organisation` as expandable even though `LearningOutcome.yaml` has no
  `organisationId`/`organisation` field. This implementation silently ignores unmatched targets in
  both cases rather than erroring.
- [**#704**](https://github.com/open-education-api/specification/issues/704) -
  `GroupInstance.yaml`'s `expand` enum on `GET /groups/{groupId}` lists only `organisation`, not
  `academic_session`, even though `Group.academicSessionId`'s own schema description documents expand
  behaviour for it - the inverse of #700 (a real, schema-declared field missing from its operation's
  `expand` enum, rather than an enum value with no backing field). This implementation already
  supports `expand=academic_session` on this endpoint regardless of the missing enum entry.
- [**#701**](https://github.com/open-education-api/specification/issues/701) -
  `AcademicSessionCourseOfferingCollection.yaml`'s `200` response declares `items` as a direct `$ref`
  to `CourseOffering`, missing the `type: array` wrapper every sibling nested-collection endpoint
  correctly has. This implementation already returns the correct, array-shaped response regardless of
  the buggy schema - no code change needed here.
- [**#702**](https://github.com/open-education-api/specification/issues/702) -
  `source/paths/LearningComponentOfferingAssociationInstance.yaml`/
  `TestComponentOfferingAssociationInstance.yaml` each contain an `otherCodes` request-body example
  whose closing `]` sits at the same indentation as the `otherCodes:` key that opens it - valid YAML
  1.1 (PyYAML tolerates it), but `redocly bundle`'s stricter parser rejects it as "deficient
  indentation," leaving both operations as unresolved, un-inlined file references in the bundled
  `oeapi.yaml`/`oeapi.json` rather than erroring the whole bundle. Any tool consuming the bundled
  artifacts (not resolving `source/` refs directly) silently loses both operations entirely, with no
  error surfaced. This implementation's own generated `spec-manifest.json`/`field-requirements.json`
  (`scripts/extract-spec-manifest.py`) are built from a copy of the `source/` tree with both
  `otherCodes` examples reformatted to plain YAML block-sequence style before bundling, so both
  operations are correctly represented here regardless of the upstream bug.
- [**#703**](https://github.com/open-education-api/specification/issues/703) -
  `CourseOffering.flexibleEntryPeriodEndDateTime` is declared `format: date`, but the spec's own
  example for it is a full date-time, and the sibling field
  `ProgrammeOffering.flexibleEntryPeriodEndDateTime` (same description, same example) is correctly
  declared `format: date-time` - a copy-paste typo. This implementation returns a date-time, matching
  the field's own example and sibling, not the buggy declared format.

## Further reading

- `DATABASE_CONFIGURATION.md` — full database provider configuration, migrations, production
  hardening.
- `AUTHENTICATION_GUIDE.md` — sample JWT/API-key provider implementations.
- `docs/archive/DECISIONS-AND-ACTIONS.md` — the reasoning behind this implementation's notable
  design choices, for anyone extending or evaluating the codebase.
- `docs/TODO-LIST.md` — genuinely outstanding items; mostly issues filed upstream and blocked on a
  spec maintainer response.

## License

Licensed under the [EUPL-1.2](LICENSE.md), the same licence the OOAPI specification itself is
published under.
