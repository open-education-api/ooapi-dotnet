using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IServiceMetadataService
    {
        Service Get(out ErrorResponse errorResponse);
    }
}