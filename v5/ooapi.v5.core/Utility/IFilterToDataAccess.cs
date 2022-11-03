namespace ooapi.v5.core.Utility
{
    interface IFilterToDataAccess<T>
    {
        IQueryable<T> Parse(IQueryable<T> collection);
    }
}
