using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class RoomsService : ServiceBase
    {
        private readonly RoomsRepository _repository;

        public RoomsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new RoomsRepository(dbContext);
        }

        public Pagination<Room> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<Room> result = _repository.GetAllOrderedBy(dataRequestParameters);
                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Room Get(Guid roomId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetRoom(roomId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<Room> GetRoomsByBuildingId(DataRequestParameters dataRequestParameters, Guid buildingId, out ErrorResponse errorResponse)
        {
            try
            {
                var result = _repository.GetRoomsByBuildingId(buildingId);
                var paginationResult = new Pagination<Room>(result.AsQueryable(), dataRequestParameters);
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
