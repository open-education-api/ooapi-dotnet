using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IProgramOfferingService
{
    ProgramOffering? Get(Guid programOfferingId);
    Task<Pagination<ProgramOffering>> GetAll(DataRequestParameters dataRequestParameters);
}