namespace ooapi.v5.core.Security;

public interface IUserRequestContext
{
    string Bivv { get; set; }
    string CurOrganisation { get; set; }
    bool IsEmployee { get; set; }
    bool IsLocal { get; set; }
    bool IsStudent { get; set; }
    string UserID { get; set; }
}