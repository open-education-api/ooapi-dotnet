using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IEducationSpecificationsService
    {
        EducationSpecification Get(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<EducationSpecification> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<EducationSpecification> GetEducationSpecificationsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse errorResponse);
        Pagination<EducationSpecification> GetEducationSpecificationsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse);
    }
}