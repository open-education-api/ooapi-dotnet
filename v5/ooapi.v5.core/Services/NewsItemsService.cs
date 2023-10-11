using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class NewsItemsService : ServiceBase, INewsItemsService
    {
        private readonly NewsItemsRepository _repository;

        public NewsItemsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new NewsItemsRepository(dbContext);
        }

        public NewsItem Get(Guid newsitemId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetNewsItem(newsitemId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<NewsItem> GetNewsItemsByNewsFeedId(DataRequestParameters dataRequestParameters, Guid newsfeedId, out ErrorResponse errorResponse)
        {
            try
            {
                var result = _repository.GetNewsItemsByNewsFeedId(newsfeedId);
                var paginationResult = new Pagination<NewsItem>(result.AsQueryable(), dataRequestParameters);
                errorResponse = null;
                return paginationResult;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }
    }
}
