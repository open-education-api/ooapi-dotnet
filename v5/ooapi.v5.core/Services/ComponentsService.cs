﻿using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ComponentsService : ServiceBase, IComponentsService
{
    private readonly IComponentsRepository _repository;

    public ComponentsService(ICoreDbContext dbContext, IComponentsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Component?> GetAsync(Guid componentId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetComponentAsync(componentId, cancellationToken);
    }

    public async Task<Pagination<Component>> GetComponentsByCourseIdAsync(DataRequestParameters dataRequestParameters, Guid courseId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetComponentsByCourseIdAsync(courseId, dataRequestParameters, cancellationToken);
    }

    public async Task<Pagination<Component>> GetComponentsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetComponentsByOrganizationIdAsync(organizationId, dataRequestParameters, cancellationToken);
    }
}