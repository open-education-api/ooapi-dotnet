﻿using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class ServiceMetadataService : ServiceBase
    {
        private readonly ServiceMetadataRepository _repository;

        public ServiceMetadataService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new ServiceMetadataRepository(dbContext);
        }

        public Service Get(out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetServiceMetadata();

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