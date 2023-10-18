namespace ooapi.v5.core.Security;

public interface IUserRequestContext
{
    string Bivv { get; set; }
    string CurrentOrganisation { get; set; }
    bool IsEmployee { get; set; }
    bool IsLocal { get; set; }
    bool IsStudent { get; set; }
    string UserId { get; set; }
}