﻿using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class CoursesService : ServiceBase, ICoursesService
{
    private readonly CoursesRepository _repository;

    public CoursesService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new CoursesRepository(dbContext);
    }

    public Pagination<Course> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public Course? Get(Guid courseId)
    {
        return _repository.GetCourse(courseId);
    }

    public Pagination<Course> GetCoursesByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId)
    {
        return _repository.GetCoursesByEducationSpecificationId(educationSpecificationId, dataRequestParameters);
    }

    public Pagination<Course> GetCoursesByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId)
    {
        var result = _repository.GetCoursesByOrganizationId(organizationId);
        return new Pagination<Course>(result.AsQueryable(), dataRequestParameters);
    }

    public Pagination<Course> GetCoursesByProgramId(DataRequestParameters dataRequestParameters, Guid programId)
    {
        var result = _repository.GetCoursesByProgramId(programId);
        return new Pagination<Course>(result.AsQueryable(), dataRequestParameters);
    }
}
