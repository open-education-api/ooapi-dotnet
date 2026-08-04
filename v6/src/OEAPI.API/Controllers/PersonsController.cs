using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OEAPI.API.Middleware;
using OEAPI.API.Serialization;
using OEAPI.Core.Interfaces;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Data.Mapping;
using OEAPI.Infrastructure.Query;
using OEAPI.Infrastructure.Query.Consumers;
using OEAPI.Infrastructure.Query.Extensions;
using OEAPI.Infrastructure.Query.Fields;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for person endpoints.
/// </summary>
/// <remarks>
///     Initializes a new instance of the PersonsController class.
/// </remarks>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger.</param>
/// <param name="currentPersonProvider">Resolves the "me" in <c>GET /persons/me</c>.</param>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[Tags("Persons")]
public class PersonsController(OEAPIDbContext dbContext, ILogger<PersonsController> logger,
    ICurrentPersonProvider currentPersonProvider) : GenericEntityController<PersonEntity, Person>(dbContext, logger, "Person", "persons")
{
    private readonly ICurrentPersonProvider _currentPersonProvider = currentPersonProvider;
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>Retrieves a list of all persons.</summary>
    /// <param name="primaryCode">Filter by primary code.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response for Scalar/OpenAPI doc generation -
    // TApiModel can't be referenced in an attribute on the generic base class (CS0416). Behaviour is
    // unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(PagedResult<Person>), StatusCodes.Status200OK)]
    public override Task<IActionResult> GetAll(
        string? primaryCode,
        int? pageNumber,
        int? pageSize,
        string? consumer,
        string? fields,
        CancellationToken cancellationToken = default)
    {
        return base.GetAll(primaryCode, pageNumber, pageSize, consumer, fields, cancellationToken);
    }

    /// <summary>Retrieves a single person by ID.</summary>
    /// <param name="personId">The person ID.</param>
    /// <param name="expand">Comma-separated list of relations to expand (no expandable relations are currently defined for this resource).</param>
    /// <param name="fields">The spec's nested-parens field selection syntax, e.g. <c>(id,title,programme(code))</c>.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    // Overridden only to attach a concrete-typed OpenAPI response - see the GetAll override above.
    // Behaviour is unchanged; forwards straight to the base implementation.
    [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
    [HttpGet("{personId}")]
    public override Task<IActionResult> GetById(
        string personId,
        string? expand,
        string? fields,
        string? consumer,
        CancellationToken cancellationToken = default)
    {
        return base.GetById(personId, expand, fields, consumer, cancellationToken);
    }

    /// <summary>
    ///     Applies the spec's <c>q</c> (<c>personSearch</c>) and <c>affiliations</c> filters, plus the
    ///     non-spec <c>activeEnrolment</c>/<c>gender</c> filters (kept as a documented extension - not
    ///     part of the spec, but harmless additive query params most clients will simply ignore), to a
    ///     <c>GetAll</c> query.
    /// </summary>
    protected override IQueryable<PersonEntity> ApplyEntityFilters(IQueryable<PersonEntity> query)
    {
        // Spec: q (personSearch) - partial, case-insensitive match against givenName, surnamePrefix,
        // surname, displayName, initials, email or secondaryEmail. Explicitly lower-cased on both
        // sides rather than using the shared WhereContains (which relies on each provider's default
        // LIKE collation - case-insensitive on SQL Server but case-sensitive on PostgreSQL by
        // default), since the spec mandates case-insensitivity for this specific parameter.
        string? searchValue = GetQueryValue("q");
        if (!string.IsNullOrEmpty(searchValue))
        {
            string searchTerm = searchValue.ToLower();
            query = query.Where(p =>
                (p.GivenName != null && p.GivenName.ToLower().Contains(searchTerm)) ||
                (p.SurnamePrefix != null && p.SurnamePrefix.ToLower().Contains(searchTerm)) ||
                (p.Surname != null && p.Surname.ToLower().Contains(searchTerm)) ||
                (p.DisplayName != null && p.DisplayName.ToLower().Contains(searchTerm)) ||
                (p.Initials != null && p.Initials.ToLower().Contains(searchTerm)) ||
                (p.Email != null && p.Email.ToLower().Contains(searchTerm)) ||
                (p.SecondaryEmail != null && p.SecondaryEmail.ToLower().Contains(searchTerm)));
        }

        // Spec: affiliations - a single personAffiliation value; matches persons whose affiliations
        // array contains it. Affiliations is stored as a serialized JSON array (AffiliationsJson), so
        // this matches on the array's own JSON-encoded element rather than via a typed column.
        string? affiliationsValue = GetQueryValue("affiliations");
        if (!string.IsNullOrEmpty(affiliationsValue))
        {
            string affiliationJsonElement = JsonSerializer.Serialize(affiliationsValue);
            query = query.Where(p => p.AffiliationsJson != null && p.AffiliationsJson.Contains(affiliationJsonElement));
        }

        // Non-spec extension, kept for convenience.
        string? activeEnrolmentValue = GetQueryValue("activeEnrolment");
        if (!string.IsNullOrEmpty(activeEnrolmentValue) &&
            bool.TryParse(activeEnrolmentValue, out bool activeEnrolment))
            query = query.Where(p => p.ActiveEnrolment == activeEnrolment);

        // Non-spec extension, kept for convenience.
        string? genderValue = GetQueryValue("gender");
        if (!string.IsNullOrEmpty(genderValue))
            query = query.Where(p => p.Gender != null && p.Gender.ToLower() == genderValue.ToLower());

        return query;
    }

    /// <summary>
    ///     Returns the person object for the currently authenticated user. GET /persons/me
    ///     The spec deliberately leaves the actual authentication mechanism up to the deployment
    ///     ("Security must be implemented at the level of the actual deployment rather than in the core
    ///     specification"). This reference implementation resolves the authenticated person via
    ///     <see cref="OEAPI.Core.Interfaces.ICurrentPersonProvider" /> - the extension point institutions
    ///     fill in with logic matching their own IAM (see <c>DefaultCurrentPersonProvider</c> and its
    ///     registration in <c>Program.cs</c>). No real auth provider is registered in this dev
    ///     environment (see <c>Program.cs</c>'s commented-out service registrations), so the
    ///     authentication middleware never populates a <c>UserId</c> and this correctly 401s until one is
    ///     configured.
    /// </summary>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("me")]
    [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMe(
        [FromQuery] string? fields,
        [FromQuery] string? consumer,
        CancellationToken cancellationToken = default)
    {
        try
        {
            AuthenticationResult? authResult = HttpContext.GetAuthenticationResult();
            string? userId = await _currentPersonProvider.GetCurrentPersonIdAsync(authResult, cancellationToken)
                .ConfigureAwait(false);
            if (string.IsNullOrEmpty(userId))
                return Problem(
                    detail: "No authenticated user context is available for this request.",
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Unauthorized");

            PersonEntity? personEntity = await Entities
                .FirstOrDefaultAsync(p => p.PersonId == userId, cancellationToken)
                .ConfigureAwait(false);

            if (personEntity == null)
                return Problem(
                    detail: $"No person found for the authenticated user '{userId}'.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            Person apiModel = MapToApiModel(personEntity, consumer);

            FieldSelection? fieldSelection = FieldSelectionParser.Parse(fields);
            if (fieldSelection != null)
            {
                JsonNode? node = JsonSerializer.SerializeToNode(apiModel, OeapiJsonSerializerOptions.Instance);
                FieldPruner.Prune(node, fieldSelection, nameof(Person));
                return Ok(node);
            }

            return Ok(apiModel);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Required abstract method implementations
    // ========================================================================

    /// <summary>
    ///     Maps a PersonEntity to a Person API model.
    /// </summary>
    /// <param name="entity">The entity to map.</param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    /// <returns>The API model.</returns>
    protected override Person MapToApiModel(PersonEntity entity, string? consumer)
    {
        return entity.ToApiModel(consumer);
    }

    /// <summary>
    ///     Maps a Person API model to a PersonEntity.
    /// </summary>
    /// <param name="model">The API model to map.</param>
    /// <returns>The entity.</returns>
    protected override PersonEntity MapToEntity(Person model)
    {
        return model.ToEntity();
    }

    /// <summary>
    ///     Updates a PersonEntity from a Person API model.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="model">The API model with new data.</param>
    protected override void UpdateEntityFromApiModel(PersonEntity entity, Person model)
    {
        entity.UpdateFrom(model);
    }

    /// <summary>
    ///     Gets the unique identifier for a person entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>The identifier as string.</returns>
    protected override string GetEntityId(PersonEntity entity)
    {
        return entity.PersonId;
    }

    /// <summary>
    ///     Replaces a person, or creates one at the given id if it doesn't exist yet.
    ///     PUT /persons/{personId}. Upsert semantics per spec: <c>200</c> if replacing an existing
    ///     person, <c>201</c> if creating a new one at the given id. Person has no relationship fields
    ///     of its own, so no FK resolution is needed here.
    /// </summary>
    /// <param name="personId">The person ID.</param>
    /// <param name="model">The full person representation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{personId}")]
    // No typeof() here: this action returns Ok()/StatusCode(201) with no body - see the identical
    // note on OrganisationsController.PutOrganisation.
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutPerson(
        string personId,
        [FromBody] Person model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            PersonEntity? entity = await _dbContext.Persons
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);

            bool isNew;
            if (entity == null)
            {
                isNew = true;
                entity = MapToEntity(model);
                entity.PersonId = personId;
                _dbContext.Persons.Add(entity);
            }
            else
            {
                isNew = false;
                UpdateEntityFromApiModel(entity, model);
            }

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return isNew ? StatusCode(StatusCodes.Status201Created) : Ok();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Creates a new person. POST /persons. Takes the spec's narrower <see cref="PersonProperties" />
    ///     request shape (no <c>personId</c> - the server generates it) and responds with the spec's
    ///     <c>PersonId</c> + <c>PostResponse</c> composed shape (<see cref="PostResponse" />), not
    ///     the full <see cref="Person" /> resource - the same "narrower response than the full resource"
    ///     shape already used by <see cref="AssociationWriteResponse" /> for this codebase's other POST
    ///     endpoints.
    /// </summary>
    /// <param name="model">The new person's properties.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePerson(
        [FromBody] PersonProperties model,
        [FromQuery] string? consumer,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // PersonEntity has a unique index on (PrimaryCodeType, PrimaryCode) - primaryCode is
            // optional per spec, but every omitting client can't fall back to the same empty string
            // without colliding on the second such person, so an omitted code value falls back to
            // the newly generated PersonId (already unique) instead.
            string personId = Guid.NewGuid().ToString();
            PersonEntity entity = new()
            {
                PersonId = personId,
                PrimaryCodeType = model.PrimaryCode?.CodeType ?? string.Empty,
                PrimaryCode = model.PrimaryCode?.Code ?? personId,
                GivenName = model.GivenName,
                AlternateName = model.AlternateName,
                PreferredName = model.PreferredName,
                SurnamePrefix = model.SurnamePrefix,
                Surname = model.Surname,
                DisplayName = model.DisplayName,
                Initials = model.Initials,
                IdCheckName = model.IdCheckName,
                ActiveEnrolment = model.ActiveEnrolment,
                DateOfBirth = model.DateOfBirth,
                CityOfBirth = model.CityOfBirth,
                CountryOfBirthJson =
                    model.CountryOfBirth != null ? JsonSerializer.Serialize(model.CountryOfBirth) : null,
                NationalityJson = model.Nationality != null ? JsonSerializer.Serialize(model.Nationality) : null,
                DateOfNationality = model.DateOfNationality,
                Gender = model.Gender?.ToLower(),
                TitlePrefix = model.TitlePrefix,
                TitleSuffix = model.TitleSuffix,
                Office = model.Office,
                Email = model.Email,
                SecondaryEmail = model.SecondaryEmail,
                TelephoneNumber = model.TelephoneNumber,
                MobileNumber = model.MobileNumber,
                PhotoSocial = model.PhotoSocial,
                PhotoOfficial = model.PhotoOfficial,
                IceName = model.IceName,
                IcePhoneNumber = model.IcePhoneNumber,
                AffiliationsJson = model.Affiliations != null ? JsonSerializer.Serialize(model.Affiliations) : null,
                LanguageOfChoiceJson = model.LanguageOfChoice != null
                    ? JsonSerializer.Serialize(model.LanguageOfChoice)
                    : null,
                OtherCodes =
                    model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                        .ToList() ?? [],
                AssignedNeedsJson = model.AssignedNeeds != null ? JsonSerializer.Serialize(model.AssignedNeeds) : null,
                AddressJson = model.Address != null ? JsonSerializer.Serialize(model.Address) : null,
                IceRelationJson = model.IceRelation != null ? JsonSerializer.Serialize(model.IceRelation) : null,
                ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : null,
                ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
                ConsumerKey = consumer,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _dbContext.Persons.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return StatusCode(StatusCodes.Status201Created, new PostResponse
            {
                PersonId = entity.PersonId,
                Message =
                [
                    new LanguageTypedString { Language = "en-GB", Value = "Person created successfully." }
                ]
            });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Custom methods for PersonsController
    // ========================================================================

    /// <summary>
    ///     Applies default ordering for persons: by surname, then by given name.
    /// </summary>
    /// <param name="query">The query to order.</param>
    /// <returns>The ordered query.</returns>
    protected override IQueryable<PersonEntity> ApplyDefaultOrdering(IQueryable<PersonEntity> query)
    {
        return query.OrderBy(p => p.Surname).ThenBy(p => p.GivenName);
    }

    // ========================================================================
    // Nested endpoints for persons/{personId}/course-offering-associations
    // ========================================================================

    /// <summary>
    ///     Retrieves all course offering associations for a specific person.
    ///     GET /api/persons/{personId}/course-offering-associations
    /// </summary>
    /// <param name="personId">The person ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="search">Search term.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="expand">Comma-separated list of relations to expand, e.g. <c>academicSession,courseOffering</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{personId}/course-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<CourseOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCourseOfferingAssociationsByPersonId(
        string personId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? search,
        [FromQuery] string? state,
        [FromQuery] string? role,
        [FromQuery] string? resultState,
        [FromQuery] string? expand,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the person exists
            PersonEntity? personEntity = await _dbContext.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);

            if (personEntity == null)
                return Problem(
                    detail: $"Person with ID '{personId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get course offering associations for this person
            IQueryable<CourseOfferingAssociationEntity> query = _dbContext.CourseOfferingAssociations
                .AsNoTracking()
                .Include(coa => coa.CourseOffering)
                .Include(coa => coa.Person)
                .Include(coa => coa.Organisation)
                .Where(coa => coa.PersonEntityId != null && coa.PersonEntityId == personEntity.Id);

            // Apply generic filtering
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(search))
                query = query.WhereContains(search, nameof(CourseOfferingAssociationEntity.PrimaryCode));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply state filter
            if (!string.IsNullOrEmpty(state)) query = query.Where(coa => coa.State != null && coa.State == state);

            // Apply role filter
            if (!string.IsNullOrEmpty(role)) query = query.Where(coa => coa.Role != null && coa.Role == role);

            // Apply default ordering: by primary code
            query = query.OrderBy(coa => coa.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (see design.md Decision 5), so every
            // association matching the SQL-level filters above is mapped first, then filtered by
            // resultState in memory, and only then paginated - preserving correct totalCount/paging
            // instead of paginating before this filter and under-filling pages.
            List<CourseOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<CourseOfferingAssociation> candidates = [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<CourseOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            // Apply expand, per item, after pagination - reuses
            // CourseOfferingAssociationsController.ApplyExpandAsync's "courseoffering" case body, plus
            // a new "academicsession" case (see design.md Decision 6: this endpoint is the only place
            // CourseOfferingAssociation.AcademicSession is ever populated).
            if (!string.IsNullOrEmpty(expand))
                foreach (CourseOfferingAssociation apiModel in apiModels)
                    await ApplyCourseOfferingAssociationExpandAsync(apiModel, expand, cancellationToken)
                        .ConfigureAwait(false);

            // Create paged result
            PagedResult<CourseOfferingAssociation> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    ///     Applies <c>expand</c> to a single <see cref="CourseOfferingAssociation" /> returned from
    ///     <see cref="GetCourseOfferingAssociationsByPersonId" />. <c>courseoffering</c> duplicates
    ///     <c>CourseOfferingAssociationsController.ApplyExpandAsync</c>'s case body verbatim;
    ///     <c>academicsession</c> is new here - see design.md Decision 6 (in the
    ///     <c>fix-nested-endpoint-spec-param-gaps</c> change) for why this field exists only on this one
    ///     endpoint's response.
    /// </summary>
    private async Task ApplyCourseOfferingAssociationExpandAsync(
        CourseOfferingAssociation apiModel,
        string expand,
        CancellationToken cancellationToken)
    {
        string[] expands = expand.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expandOption in expands)
            switch (expandOption.Trim().ToLowerInvariant())
            {
                case "course_offering":
                    if (apiModel.CourseOfferingId != null && apiModel.CourseOffering == null)
                    {
                        CourseOfferingEntity? courseOfferingEntity = await _dbContext.CourseOfferings
                            .AsNoTracking()
                            .Include(co => co.Groups)
                            .Include(co => co.ProgrammeOfferings)
                            .Include(co => co.Course)
                            .Include(co => co.Organisation)
                            .Include(co => co.AcademicSession)
                            .FirstOrDefaultAsync(co =>
                                    co.CourseOfferingId == apiModel.CourseOfferingId.Value ||
                                    co.Id.ToString() == apiModel.CourseOfferingId.Value,
                                cancellationToken)
                            .ConfigureAwait(false);

                        if (courseOfferingEntity != null)
                            apiModel.CourseOffering = courseOfferingEntity.ToApiModel(null);
                    }

                    break;

                case "academic_session":
                    if (apiModel.CourseOfferingId != null && apiModel.AcademicSession == null)
                    {
                        AcademicSessionEntity? academicSessionEntity = await _dbContext.CourseOfferings
                            .AsNoTracking()
                            .Where(co =>
                                co.CourseOfferingId == apiModel.CourseOfferingId.Value ||
                                co.Id.ToString() == apiModel.CourseOfferingId.Value)
                            .Select(co => co.AcademicSession)
                            .FirstOrDefaultAsync(cancellationToken)
                            .ConfigureAwait(false);

                        if (academicSessionEntity != null)
                            apiModel.AcademicSession = academicSessionEntity.ToApiModel(null);
                    }

                    break;
            }
    }

    // ========================================================================
    // Nested endpoints for persons/{personId}/learning-component-offering-associations
    // ========================================================================

    /// <summary>
    ///     Retrieves all learning component offering associations for a specific person.
    ///     GET /api/persons/{personId}/learning-component-offering-associations
    /// </summary>
    /// <param name="personId">The person ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="search">Search term.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{personId}/learning-component-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<LearningComponentOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLearningComponentOfferingAssociationsByPersonId(
        string personId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? search,
        [FromQuery] string? state,
        [FromQuery] string? role,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the person exists
            PersonEntity? personEntity = await _dbContext.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);

            if (personEntity == null)
                return Problem(
                    detail: $"Person with ID '{personId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get learning component offering associations for this person
            IQueryable<LearningComponentOfferingAssociationEntity> query = _dbContext
                .LearningComponentOfferingAssociations
                .AsNoTracking()
                .Include(lcoa => lcoa.LearningComponentOffering)
                .Include(lcoa => lcoa.Person)
                .Include(lcoa => lcoa.Organisation)
                .Where(lcoa => lcoa.PersonEntityId != null && lcoa.PersonEntityId == personEntity.Id);

            // Apply generic filtering
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(search))
                query = query.WhereContains(search, nameof(LearningComponentOfferingAssociationEntity.PrimaryCode));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply state filter
            if (!string.IsNullOrEmpty(state)) query = query.Where(lcoa => lcoa.State != null && lcoa.State == state);

            // Apply role filter
            if (!string.IsNullOrEmpty(role)) query = query.Where(lcoa => lcoa.Role != null && lcoa.Role == role);

            // Apply default ordering: by primary code
            query = query.OrderBy(lcoa => lcoa.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (see design.md Decision 5), so every
            // association matching the SQL-level filters above is mapped first, then filtered by
            // resultState in memory, and only then paginated - preserving correct totalCount/paging
            // instead of paginating before this filter and under-filling pages.
            List<LearningComponentOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<LearningComponentOfferingAssociation> candidates =
                [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<LearningComponentOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            // Create paged result
            PagedResult<LearningComponentOfferingAssociation> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Nested endpoints for persons/{personId}/programme-offering-associations
    // ========================================================================

    /// <summary>
    ///     Retrieves all programme offering associations for a specific person.
    ///     GET /api/persons/{personId}/programme-offering-associations
    /// </summary>
    /// <param name="personId">The person ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="search">Search term.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{personId}/programme-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<ProgrammeOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProgrammeOfferingAssociationsByPersonId(
        string personId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? search,
        [FromQuery] string? state,
        [FromQuery] string? role,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the person exists
            PersonEntity? personEntity = await _dbContext.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);

            if (personEntity == null)
                return Problem(
                    detail: $"Person with ID '{personId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get programme offering associations for this person
            IQueryable<ProgrammeOfferingAssociationEntity> query = _dbContext.ProgrammeOfferingAssociations
                .AsNoTracking()
                .Include(poa => poa.ProgrammeOffering)
                .Include(poa => poa.Person)
                .Include(poa => poa.Organisation)
                .Where(poa => poa.PersonEntityId != null && poa.PersonEntityId == personEntity.Id);

            // Apply generic filtering
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(search))
                query = query.WhereContains(search, nameof(ProgrammeOfferingAssociationEntity.PrimaryCode));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply state filter
            if (!string.IsNullOrEmpty(state)) query = query.Where(poa => poa.State != null && poa.State == state);

            // Apply role filter
            if (!string.IsNullOrEmpty(role)) query = query.Where(poa => poa.Role != null && poa.Role == role);

            // Apply default ordering: by primary code
            query = query.OrderBy(poa => poa.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (see design.md Decision 5), so every
            // association matching the SQL-level filters above is mapped first, then filtered by
            // resultState in memory, and only then paginated - preserving correct totalCount/paging
            // instead of paginating before this filter and under-filling pages.
            List<ProgrammeOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<ProgrammeOfferingAssociation> candidates =
                [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<ProgrammeOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            // Create paged result
            PagedResult<ProgrammeOfferingAssociation> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Nested endpoints for persons/{personId}/test-component-offering-associations
    // ========================================================================

    /// <summary>
    ///     Retrieves all test component offering associations for a specific person.
    ///     GET /api/persons/{personId}/test-component-offering-associations
    /// </summary>
    /// <param name="personId">The person ID.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="consumer">Consumer key for consumer-specific data.</param>
    /// <param name="fields">Comma-separated list of fields to include.</param>
    /// <param name="search">Search term.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="role">Filter by the person's role in the association.</param>
    /// <param name="resultState">Filter by the association result's state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{personId}/test-component-offering-associations")]
    [ProducesResponseType(typeof(PagedResult<TestComponentOfferingAssociation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestComponentOfferingAssociationsByPersonId(
        string personId,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] string? consumer,
        [FromQuery(Name = "fields")] string? fields,
        [FromQuery] string? search,
        [FromQuery] string? state,
        [FromQuery] string? role,
        [FromQuery] string? resultState,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // First, verify the person exists
            PersonEntity? personEntity = await _dbContext.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken)
                .ConfigureAwait(false);

            if (personEntity == null)
                return Problem(
                    detail: $"Person with ID '{personId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            // Get test component offering associations for this person
            IQueryable<TestComponentOfferingAssociationEntity> query = _dbContext.TestComponentOfferingAssociations
                .AsNoTracking()
                .Include(tcoa => tcoa.TestComponentOffering)
                .Include(tcoa => tcoa.Person)
                .Include(tcoa => tcoa.Organisation)
                .Where(tcoa => tcoa.PersonEntityId != null && tcoa.PersonEntityId == personEntity.Id);

            // Apply generic filtering
            query = ApplyFilterQuery(query);

            // Apply search if requested
            if (!string.IsNullOrEmpty(search))
                query = query.WhereContains(search, nameof(TestComponentOfferingAssociationEntity.PrimaryCode));

            // Apply consumer filtering
            query = ConsumerKeyFilter.Apply(query, consumer);

            // Apply state filter
            if (!string.IsNullOrEmpty(state)) query = query.Where(tcoa => tcoa.State != null && tcoa.State == state);

            // Apply role filter
            if (!string.IsNullOrEmpty(role)) query = query.Where(tcoa => tcoa.Role != null && tcoa.Role == role);

            // Apply default ordering: by primary code
            query = query.OrderBy(tcoa => tcoa.PrimaryCode);

            // resultState filters on Result.State, deserialized from the opaque ResultJson blob - no
            // portable SQL-level query exists for either provider (see design.md Decision 5), so every
            // association matching the SQL-level filters above is mapped first, then filtered by
            // resultState in memory, and only then paginated - preserving correct totalCount/paging
            // instead of paginating before this filter and under-filling pages.
            List<TestComponentOfferingAssociationEntity> candidateEntities = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<TestComponentOfferingAssociation> candidates =
                [.. candidateEntities.Select(item => item.ToApiModel(consumer))];

            if (!string.IsNullOrEmpty(resultState))
                candidates = [.. candidates.Where(c => c.Result != null && c.Result.State == resultState)];

            int totalCount = candidates.Count;
            int validatedPage = Math.Max(1, pageNumber ?? 1);
            int validatedPageSize = Math.Min(Math.Max(1, pageSize ?? 20), 100);

            List<TestComponentOfferingAssociation> apiModels = [.. candidates
                .Skip(ComputePageOffset(validatedPage, validatedPageSize))
                .Take(validatedPageSize)];

            // Create paged result
            PagedResult<TestComponentOfferingAssociation> result = new(
                apiModels, totalCount, validatedPage, validatedPageSize);

            return PagedResponseWithFieldSelection(result, fields);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    // ========================================================================
    // Helper methods for nested endpoints
    // ========================================================================
}
