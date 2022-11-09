using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class EducationSpecificationsRepository : BaseRepository<EducationSpecification>
{
    public EducationSpecificationsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public EducationSpecification GetEducationSpecification(Guid educationSpecificationId)
    {
        return dbContext.EducationSpecifications.FirstOrDefault(x => x.EducationSpecificationId.Equals(educationSpecificationId));
    }

}
