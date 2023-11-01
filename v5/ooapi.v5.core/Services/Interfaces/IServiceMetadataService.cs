using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IServiceMetadataService
{
    Task<Service> GetAsync(CancellationToken cancellationToken = default);
}