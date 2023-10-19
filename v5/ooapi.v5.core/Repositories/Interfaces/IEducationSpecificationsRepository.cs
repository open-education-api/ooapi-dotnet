using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IEducationSpecificationsRepository
{
    Pagination<EducationSpecification> GetAllOrderedBy(DataRequestParameters dataRequestParameters);
    EducationSpecification GetEducationSpecification(Guid educationSpecificationId, DataRequestParameters dataRequestParameters);
    List<EducationSpecification> GetEducationSpecificationsByEducationSpecificationId(Guid educationSpecificationId);
    List<EducationSpecification> GetEducationSpecificationsByOrganizationId(Guid organizationId);
}