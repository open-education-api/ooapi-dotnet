namespace ooapi.v5.Models;


/// <typeparam name="T"></typeparam>
public interface IPagination<T>
{

    int CurrentPage { get; set; }


    bool HasNextPage { get; }


    bool HasPreviousPage { get; }


    int PageNumber { get; set; }


    int PageSize { get; set; }


    List<T>? PaginationItems { get; set; }


    int TotalPages { get; }


    void SetExtendedAttributes();
}