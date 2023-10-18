using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;


public class PersonsService : ServiceBase, IPersonsService
{
    private readonly PersonsRepository _repository;

    public PersonsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new PersonsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Person>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetAllOrderedBy(dataRequestParameters);
            errorResponse = null;
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Person? Get(Guid personId, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetPerson(personId);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="groupId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Person>? GetPersonsByGroupId(DataRequestParameters dataRequestParameters, Guid groupId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetPersonsByGroupId(groupId);
            var paginationResult = new Pagination<Person>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
