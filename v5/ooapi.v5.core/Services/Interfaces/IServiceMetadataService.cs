﻿using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IServiceMetadataService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Service? Get(out ErrorResponse? errorResponse);
}