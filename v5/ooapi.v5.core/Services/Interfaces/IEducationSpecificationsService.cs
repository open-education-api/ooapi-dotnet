using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IEducationSpecificationsService
{

    /// <param name="educationSpecificationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    EducationSpecification? Get(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<EducationSpecification>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="educationSpecificationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<EducationSpecification>? GetEducationSpecificationsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse? errorResponse);
    

    /// <param name="dataRequestParameters"></param>
    /// <param name="organizationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<EducationSpecification>? GetEducationSpecificationsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse? errorResponse);
}