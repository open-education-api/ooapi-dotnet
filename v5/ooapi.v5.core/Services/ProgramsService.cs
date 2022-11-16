﻿using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class ProgramsService : ServiceBase
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

        public Program Get(Guid programId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetProgram(programId);

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
