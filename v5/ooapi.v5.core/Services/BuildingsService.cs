using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class BuildingsService : ServiceBase
    {
        private readonly BuildingsRepository _repository;

        public BuildingsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new BuildingsRepository(dbContext);
        }

        public Pagination<Building> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                var set = _repository.GetAllBuildings();
                Pagination<Building> result = _repository.GetAllOrderedBy(dataRequestParameters, set);

                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        //private IQueryable<Model.Building> GetModelsFromEntities(IEnumerable<Building> entities)
        //{
        //    List<Model.Building> result = new List<Model.Building>();
        //    foreach (var item in entities)
        //    {
        //        result.Add(GetModelFromEntity(item));
        //    }
        //    return result.AsQueryable();
        //}

        //private Building GetModelFromEntity(Building item)
        //{
        //    Model.Address address = new Model.Address(new Model.Geolocation(item.Latitude, item.Longitude))
        //    {
        //        Type = AddressType.Postal,
        //        Street = !string.IsNullOrEmpty(item.Street) ? item.Street : null,
        //        StreetNumber = !string.IsNullOrEmpty(item.StreetNumber) ? item.StreetNumber : null,
        //        PostalCode = !string.IsNullOrEmpty(item.PostalCode) ? item.PostalCode : null,
        //        City = !string.IsNullOrEmpty(item.City) ? item.City : null
        //    };
        //    Model.Building result = new Model.Building(address)
        //    {
        //        BuildingId = item.BuildingId,
        //        Name = item.Name,
        //        Description = !string.IsNullOrEmpty(item.Description) ? item.Description : null
        //    };

        //    return result;
        //}

        public Building Get(Guid buildingId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetBuilding(buildingId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
            //var result = GetModelFromEntity(item);
            //HideAttributesBasedOnBivLevel(result, userRequestContext);

        }

    }
}
