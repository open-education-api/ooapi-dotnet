using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IOfferingsService
{
    Task<OneOfOfferingInstance?> GetAsync(Guid offeringId, CancellationToken cancellationToken = default);
}