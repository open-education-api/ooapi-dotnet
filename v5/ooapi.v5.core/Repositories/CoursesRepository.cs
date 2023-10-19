﻿using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;


public class CoursesRepository : BaseRepository<Course>
{
    public CoursesRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }


    /// <param name="courseId"></param>
    /// <returns></returns>
    public Course? GetCourse(Guid courseId)
    {
        return dbContext.Courses.Include(x => x.Attributes).FirstOrDefault(x => x.CourseId.Equals(courseId));
    }


    public Pagination<Course> GetCoursesByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<Course> set = dbContext.CoursesNoTracking.Where(o => o.EducationSpecificationId.Equals(educationSpecificationId)).Include(x => x.Attributes);
        if (!string.IsNullOrEmpty(dataRequestParameters.Consumer))
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
            
        }

        return GetAllOrderedBy(dataRequestParameters, set);
    }

    public List<Course> GetCoursesByOrganizationId(Guid organizationId)
    {
        return dbContext.Courses.Include(x => x.Attributes).Where(o => o.OrganizationId.Equals(organizationId)).ToList();
    }

    public List<Course> GetCoursesByProgramId(Guid programId)
    {
        throw new NotImplementedException();
    }
}