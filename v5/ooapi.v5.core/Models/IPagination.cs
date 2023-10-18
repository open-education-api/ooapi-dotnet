namespace ooapi.v5.Models;


/// <typeparam name="T"></typeparam>
public interface IPagination<T>
{
    /// <summary>
    /// 
    /// </summary>
    int CurrentPage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    bool HasNextPage { get; }

    /// <summary>
    /// 
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// 
    /// </summary>
    int PageNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int PageSize { get; set; }

    /// <summary>
    /// 
    /// </summary>
    List<T>? PaginationItems { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// 
    /// </summary>
    void SetExtendedAttributes();
}