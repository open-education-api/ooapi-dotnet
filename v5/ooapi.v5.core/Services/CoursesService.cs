using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class CoursesService : ServiceBase, ICoursesService
    {
        private readonly CoursesRepository _repository;

        public CoursesService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new CoursesRepository(dbContext);
        }

        public Pagination<Course> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<Course> result = _repository.GetAllOrderedBy(dataRequestParameters);
                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Course Get(Guid courseId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetCourse(courseId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<Course> GetCoursesByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<Course> result = _repository.GetCoursesByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<Course> GetCoursesByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse)
        {
            try
            {
                var result = _repository.GetCoursesByOrganizationId(organizationId);
                var paginationResult = new Pagination<Course>(result.AsQueryable(), dataRequestParameters);
                errorResponse = null;
                return paginationResult;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<Course> GetCoursesByProgramId(DataRequestParameters dataRequestParameters, Guid programId, out ErrorResponse errorResponse)
        {
            try
            {
                var result = _repository.GetCoursesByProgramId(programId);
                var paginationResult = new Pagination<Course>(result.AsQueryable(), dataRequestParameters);
                errorResponse = null;
                return paginationResult;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }
    }
}
