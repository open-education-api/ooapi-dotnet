using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

public class AssociationsService : ServiceBase, IAssociationsService
{
    private readonly AssociationsRepository _repository;

    public AssociationsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new AssociationsRepository(dbContext);
    }

    public Association Get(Guid associationId, out ErrorResponse errorResponse)
    {
        try
        {
            var item = _repository.GetAssociation(associationId);

            errorResponse = null;
            return item;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Association> GetAssociationsByPersonId(DataRequestParameters dataRequestParameters, Guid personId, out ErrorResponse errorResponse)
    {
        try
        {
            var result = _repository.GetAssociationsByPersonId(personId);
            var paginationResult = new Pagination<Association>(result.AsQueryable(), dataRequestParameters);
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
