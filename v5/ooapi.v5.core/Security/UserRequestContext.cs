using Microsoft.AspNetCore.Http;

namespace ooapi.v5.core.Security;
public class UserRequestContext : IUserRequestContext
{
    public UserRequestContext(IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(nameof(httpContextAccessor));

        var request = httpContextAccessor.HttpContext!.Request;
        var curHeaders = request.Headers;

        if (curHeaders.TryGetValue("userId", out var headerUserId))
        {
            if (headerUserId.Count > 0)
                UserID = headerUserId[0].ToString();
        }

        if (curHeaders.TryGetValue("isStudent", out var headerIsStudent))
        {
            if (headerIsStudent.Count > 0)
                IsStudent = Convert.ToBoolean(headerIsStudent[0].ToString());
        }

        if (curHeaders.TryGetValue("IsEmployee", out var headerIsEmployee))
        {
            if (headerIsEmployee.Count > 0)
                IsEmployee = Convert.ToBoolean(headerIsEmployee[0].ToString());
        }

        if (curHeaders.TryGetValue("Bivv", out var headerBivv))
        {
            if (headerBivv.Count > 0)
                Bivv = String.IsNullOrEmpty(headerBivv[0].ToString()) ? headerBivv[0].ToString() : "laag";
        }

        IsLocal = request.Host.Host.ToLower().Equals("localhost");
    }

    public string UserID { get; set; }
    public bool IsStudent { get; set; }
    public bool IsEmployee { get; set; }
    public string Bivv { get; set; }
    public string CurOrganisation { get; set; }
    public bool IsLocal { get; set; }
}
