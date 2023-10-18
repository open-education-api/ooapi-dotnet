﻿using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IOrganizationsService
{

    /// <param name="organizationId"></param>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Organization? Get(Guid organizationId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Organization>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);
}