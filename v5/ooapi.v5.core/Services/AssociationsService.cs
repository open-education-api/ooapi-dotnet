﻿using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class AssociationsService : ServiceBase
    {
        private readonly AssociationsRepository _repository;

        public AssociationsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new AssociationsRepository(dbContext);
        }

        public Association Get(Guid associationId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetAssociation(associationId);

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