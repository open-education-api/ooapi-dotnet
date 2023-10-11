using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class NewsFeedsService : ServiceBase, INewsFeedsService
    {
        private readonly NewsFeedsRepository _repository;

        public NewsFeedsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new NewsFeedsRepository(dbContext);
        }

        public Pagination<NewsFeed> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<NewsFeed> result = _repository.GetAllOrderedBy(dataRequestParameters);
                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public NewsFeed Get(Guid newsfeedId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetNewsFeed(newsfeedId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

    }
}
