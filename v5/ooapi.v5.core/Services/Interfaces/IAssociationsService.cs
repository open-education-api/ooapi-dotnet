using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IAssociationsService
{
    Association? Get(Guid associationId);
    Pagination<Association> GetAssociationsByPersonId(DataRequestParameters dataRequestParameters, Guid personId);
}
