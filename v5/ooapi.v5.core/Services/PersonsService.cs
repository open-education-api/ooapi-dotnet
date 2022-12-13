using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class PersonsService : ServiceBase
    {
        private readonly PersonsRepository _repository;

        public PersonsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new PersonsRepository(dbContext);
        }

        public Pagination<Person> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<Person> result = _repository.GetAllOrderedBy(dataRequestParameters);
                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Person Get(Guid personId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetPerson(personId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<Person> GetPersonsByGroupId(DataRequestParameters dataRequestParameters, Guid groupId, out ErrorResponse errorResponse)
        {
            try
            {
                var result = _repository.GetPersonsByGroupId(groupId);
                var paginationResult = new Pagination<Person>(result.AsQueryable(), dataRequestParameters);
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
