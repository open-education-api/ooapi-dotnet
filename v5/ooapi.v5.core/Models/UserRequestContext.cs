namespace ooapi.v5.core.Models
{
    public class UserRequestContext
    {
        private string _userID = "";
        private bool _isStudent = false;
        private bool _isEmployee = false;
        private string _bivv = "";
        private string _curOrganisation = "";
        private bool _isLocal = false;

        public string UserID { get => _userID; set => _userID = value; }
        public bool IsStudent { get => _isStudent; set => _isStudent = value; }
        public bool IsEmployee { get => _isEmployee; set => _isEmployee = value; }
        public string Bivv { get => _bivv; set => _bivv = value; }
        public string CurOrganisation { get => _curOrganisation; set => _curOrganisation = value; }
        public bool IsLocal { get => _isLocal; set => _isLocal = value; }
    }
}
