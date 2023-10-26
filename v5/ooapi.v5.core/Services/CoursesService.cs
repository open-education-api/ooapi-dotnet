using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class CoursesService : ServiceBase, ICoursesService
{
    private readonly ICoursesRepository _repository;

    public CoursesService(ICoreDbContext dbContext, ICoursesRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<Course>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, null, cancellationToken);
    }

    public async Task<Course?> GetAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetCourseAsync(courseId, cancellationToken);
    }

    public async Task<Pagination<Course>> GetCoursesByEducationSpecificationIdAsync(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetCoursesByEducationSpecificationIdAsync(educationSpecificationId, dataRequestParameters, cancellationToken);
    }

    public async Task<Pagination<Course>> GetCoursesByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetCoursesByOrganizationIdAsync(organizationId, cancellationToken);
        return new Pagination<Course>(result.AsQueryable(), dataRequestParameters);
    }

    public async Task<Pagination<Course>> GetCoursesByProgramIdAsync(DataRequestParameters dataRequestParameters, Guid programId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetCoursesByProgramIdAsync(programId, cancellationToken);
        return new Pagination<Course>(result.AsQueryable(), dataRequestParameters);
    }
}