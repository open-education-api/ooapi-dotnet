﻿using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class BuildingsService : ServiceBase, IBuildingsService
{
    private readonly IBuildingsRepository _repository;

    public BuildingsService(ICoreDbContext dbContext, IBuildingsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<Building>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, cancellationToken);
    }

    public async Task<Building?> GetAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetBuildingAsync(buildingId, cancellationToken);
    }
}