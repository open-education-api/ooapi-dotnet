using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class EducationSpecificationsService : ServiceBase
    {
        readonly EducationSpecificationsRepository repository;

        public EducationSpecificationsService(EducationSpecificationsRepository repository, UserRequestContext userRequestContext) : base(userRequestContext)
        {
            this.repository = repository;
        }

        public EducationSpecifications GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<EducationSpecification> educationSpecifications = repository.GetAllOrderedBy(dataRequestParameters);

                errorResponse = null;
                return (EducationSpecifications)educationSpecifications;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public EducationSpecification Get(Guid educationSpecificationId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = repository.GetEducationSpecification(educationSpecificationId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

    }
}
