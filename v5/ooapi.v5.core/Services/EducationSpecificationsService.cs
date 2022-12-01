﻿using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class EducationSpecificationsService : ServiceBase
    {
        private readonly EducationSpecificationsRepository _repository;

        public EducationSpecificationsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new EducationSpecificationsRepository(dbContext);
        }

        public Pagination<EducationSpecification> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<EducationSpecification> result = _repository.GetAllOrderedBy(dataRequestParameters);
                errorResponse = null;
                return result;
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
                var item = _repository.GetEducationSpecification(educationSpecificationId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public Pagination<EducationSpecification> GetEducationSpecificationsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse errorResponse)
        {
            try
            {
                var result = _repository.GetEducationSpecificationsByEducationSpecificationId(educationSpecificationId);
                var paginationResult = new Pagination<EducationSpecification>(result.AsQueryable(), dataRequestParameters);
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
