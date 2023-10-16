using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IAssociationsService
{
    Association Get(Guid associationId, out ErrorResponse errorResponse);
    Pagination<Association> GetAssociationsByPersonId(DataRequestParameters dataRequestParameters, Guid personId, out ErrorResponse errorResponse);
}
