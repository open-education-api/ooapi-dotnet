using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace ooapi.v5.core.UnitTests.Repositories.Helpers;

public static class DbMockHelper
{
    public static void InitDb<T>(IQueryable<T> set, IQueryable<T> data) where T : class
    {
        set.Provider.Returns(data.Provider);
        set.Expression.Returns(data.Expression);
        set.ElementType.Returns(data.ElementType);
        set.GetEnumerator().Returns(data.GetEnumerator());
        set.AsNoTracking().Returns(data);
    }
}