using Microsoft.AspNetCore.Http;

namespace ooapi.v5.core.Security;
public class UserRequestContext : IUserRequestContext
{
    public UserRequestContext(IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(nameof(httpContextAccessor));

        var request = httpContextAccessor.HttpContext!.Request;
        var curHeaders = request.Headers;

        if (curHeaders.TryGetValue("userId", out var headerUserId) && headerUserId.Count > 0)
        {
            UserId = headerUserId[0].ToString();
        }

        if (curHeaders.TryGetValue("isStudent", out var headerIsStudent) && headerIsStudent.Count > 0)
        {
            IsStudent = Convert.ToBoolean(headerIsStudent[0].ToString());
        }

        if (curHeaders.TryGetValue("IsEmployee", out var headerIsEmployee) && headerIsEmployee.Count > 0)
        {
            IsEmployee = Convert.ToBoolean(headerIsEmployee[0].ToString());
        }

        if (curHeaders.TryGetValue("Bivv", out var headerBivv) && headerBivv.Count > 0 && !string.IsNullOrEmpty(headerBivv[0].ToString()))
        {
            Bivv =  headerBivv[0].ToString();
        }

        IsLocal = request.Host.Host.ToLower().Equals("localhost");
    }

    public string UserId { get; set; }
    public bool IsStudent { get; set; }
    public bool IsEmployee { get; set; }
    public string Bivv { get; set; } = "laag";
    public string CurrentOrganisation { get; set; }
    public bool IsLocal { get; set; }
}
