﻿using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class NewsFeedsService : ServiceBase, INewsFeedsService
{
    private readonly NewsFeedsRepository _repository;

    public NewsFeedsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new NewsFeedsRepository(dbContext);
    }

    public Pagination<NewsFeed> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public NewsFeed? Get(Guid newsfeedId)
{
        return _repository.GetNewsFeed(newsfeedId);
    }
}
