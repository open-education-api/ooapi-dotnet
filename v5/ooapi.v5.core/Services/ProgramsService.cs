﻿using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

public class ProgramsService : ServiceBase, IProgramsService
{
    private readonly ProgramsRepository _repository;

    public ProgramsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ProgramsRepository(dbContext);
    }

    public Pagination<Program> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
    {
        try
        {
            Pagination<Program> result = _repository.GetAllOrderedBy(dataRequestParameters);
            errorResponse = null;
            return result;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Program Get(Guid programId, DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
    {
        try
        {
            var item = _repository.GetProgram(programId, dataRequestParameters);

            errorResponse = null;
            return item;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Program> GetProgramsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse errorResponse)
    {
        try
        {
            Pagination<Program> result = _repository.GetProgramsByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
            errorResponse = null;
            return result;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Program> GetProgramsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse)
    {
        try
        {
            var result = _repository.GetProgramsByOrganizationId(organizationId);
            var paginationResult = new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Program> GetProgramsByProgramId(DataRequestParameters dataRequestParameters, Guid programId, out ErrorResponse errorResponse)
    {
        try
        {
            var result = _repository.GetProgramsByProgramId(programId);
            var paginationResult = new Pagination<Program>(result.AsQueryable(), dataRequestParameters);
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
