namespace ooapi.v5.core.Services
{
    public class BuildingsService
    {
        //readonly BuildingsRepository repository;
        //public BuildingsService(BuildingsRepository repository)
        //{
        //    this.repository = repository;
        //}

        //public ServiceResult<Model.Building> GetAll(DataRequestParameters dataRequestParameters)
        //{
        //    try
        //    {
        //        PaginatedResult<Building> buildingEntities = repository.GetAllOrderedBy(dataRequestParameters);
        //        var models = GetModelsFromEntities(buildingEntities.Items);
        //        PaginatedResult<Model.Building> buildingModels = new PaginatedResult<Model.Building>(models, null, buildingEntities.ext);
        //        buildingModels.SetPaginationMetadata(buildingEntities.GetTotalItems(), buildingEntities.PageSize, buildingEntities.PageNumber);

        //        return new ServiceResult<Model.Building>(buildingModels);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ServiceErrorResult<Model.Building>(ex);
        //    }
        //}

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

        //public Building Get(string buildingId)
        //{
        //    var item = repository.GetBuilding(buildingId);
        //    var result = GetModelFromEntity(item);
        //    //HideAttributesBasedOnBivLevel(result, userRequestContext);

        //    return result;
        //}

    }
}
