using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class ProgramsRepository : BaseRepository<Program>
{
    public ProgramsRepository(CoreDBContext dbContext) : base(dbContext)
    {
        //
    }

    public Program GetProgram(Guid programId)
    {
        return dbContext.Programs.FirstOrDefault(x => x.ProgramId.Equals(programId));
    }

    public List<Program> GetProgramsByEducationSpecificationId(Guid educationSpecificationId)
    {
        return dbContext.Programs.Where(o => o.EducationSpecification.Equals(educationSpecificationId)).ToList();
    }
}
