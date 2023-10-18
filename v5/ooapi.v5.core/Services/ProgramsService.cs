using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

/// <summary>
/// 
/// </summary>
public class ProgramsService : ServiceBase, IProgramsService
{
    private readonly ProgramsRepository _repository;

    public ProgramsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ProgramsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Program>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
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
    /// <param name="programId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Program? Get(Guid programId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetProgram(programId, dataRequestParameters);

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
    /// <param name="educationSpecificationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Program>? GetProgramsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetProgramsByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
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
    /// <param name="dataRequestParameters"></param>
    /// <param name="organizationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Program>? GetProgramsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetProgramsByOrganizationId(organizationId);
            var paginationResult = new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
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
    /// <param name="programId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Program>? GetProgramsByProgramId(DataRequestParameters dataRequestParameters, Guid programId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetProgramsByProgramId(programId);
            var paginationResult = new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
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
