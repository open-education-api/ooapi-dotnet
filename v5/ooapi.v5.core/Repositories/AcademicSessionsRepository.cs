using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Enums;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class AcademicSessionsRepository : BaseRepository<AcademicSession>, IAcademicSessionsRepository
{
    public AcademicSessionsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AcademicSession?> GetAcademicSessionAsync(Guid academicSessionId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);
        var result = await set.FirstOrDefaultAsync(x => x.AcademicSessionId.Equals(academicSessionId), cancellationToken);
        if (result == null)
        {
            return null;
        }

        return await GetAcademicSessionAsync(result, set, dataRequestParameters, cancellationToken);
    }

    public async Task<AcademicSession?> GetAcademicSessionAsync(string primaryCode, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);

        var result = await set.FirstOrDefaultAsync(x => primaryCode.Equals(x.PrimaryCode), cancellationToken);
        if (result == null)
        {
            return null;
        }

        return await GetAcademicSessionAsync(result, set, dataRequestParameters, cancellationToken);
    }

    private async Task<AcademicSession> GetAcademicSessionAsync(AcademicSession result, IQueryable<AcademicSession> set, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken)
    {
        result.ChildrenIds = await dbContext.AcademicSessionsNoTracking.Where(x => x.ParentId.Equals(result.AcademicSessionId)).Select(x => x.AcademicSessionId).ToListAsync(cancellationToken);

        if (dataRequestParameters.Expand.Contains("parent", StringComparer.InvariantCultureIgnoreCase) && result.ParentId != null)
        {
            result.Parent = await set.FirstAsync(x => x.AcademicSessionId.Equals(result.ParentId), cancellationToken);
            result.Parent.ChildrenIds = await set.Where(x => x.ParentId.Equals(result.Parent.AcademicSessionId)).Select(x => x.AcademicSessionId).ToListAsync(cancellationToken);
        }

        if (dataRequestParameters.Expand.Contains("children", StringComparer.InvariantCultureIgnoreCase))
        {
            result.Children = await set.Where(x => x.ParentId.Equals(result.AcademicSessionId)).ToListAsync(cancellationToken);
            foreach (var item in result.Children)
            {
                item.ChildrenIds = await dbContext.AcademicSessionsNoTracking.Where(x => x.ParentId.Equals(item.AcademicSessionId)).Select(x => x.AcademicSessionId).ToListAsync(cancellationToken);
            }
        }

        if (dataRequestParameters.Expand.Contains("year", StringComparer.InvariantCultureIgnoreCase) && result.YearId != null)
        {
            result.Year = await set.FirstOrDefaultAsync(x => x.AcademicSessionId.Equals(result.YearId), cancellationToken);
        }

        return result;
    }

    public async Task<Pagination<AcademicSession>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, string? academicSessionType = null, CancellationToken cancellationToken = default)
    {
        IQueryable<AcademicSession> set = dbContext.AcademicSessionsNoTracking.Include(x => x.Attributes);
        set = set.Include(x => x.Consumers.Where(y => y.ConsumerKey.Equals(dataRequestParameters.Consumer)));
        set = set.AsQueryable();

        if (academicSessionType != null)
        {
            academicSessionType = academicSessionType.Replace(" ", "_");
            if (Enum.TryParse(academicSessionType, true, out AcademicSessionType result))
            {
                set = set.Where(x => x.AcademicSessionType.Equals(result));
            }
        }
        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}