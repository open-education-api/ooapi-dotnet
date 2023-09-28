﻿using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class CourseOfferingsRepository : BaseRepository<CourseOffering>
{
    public CourseOfferingsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public CourseOffering GetCourseOffering(Guid courseOfferingId)
    {
        return dbContext.CourseOfferings.Include(x => x.Attributes).FirstOrDefault(x => x.OfferingId.Equals(courseOfferingId));
    }

    public Pagination<CourseOffering> GetCourseOfferingByProgramId(Guid courseId, DataRequestParameters dataRequestParameters)
    {
        IQueryable<CourseOffering> set = dbContext.CourseOfferingsNoTracking.Where(o => o.Course.Equals(courseId)).Include(x => x.Attributes);
        bool includeConsumer = dataRequestParameters != null && !String.IsNullOrEmpty(dataRequestParameters.Consumer);
        if (includeConsumer)
        {
            set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        }
        return GetAllOrderedBy(dataRequestParameters, set);
    }

}