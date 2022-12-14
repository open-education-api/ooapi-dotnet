﻿using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class AcademicSessionsService : ServiceBase
    {
        private readonly AcademicSessionsRepository _repository;

        public AcademicSessionsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new AcademicSessionsRepository(dbContext);
        }

        public Pagination<AcademicSession> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<AcademicSession> result = _repository.GetAllOrderedBy(dataRequestParameters);
                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public AcademicSession Get(Guid academicSessionId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetAcademicSession(academicSessionId);

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