using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class RegisterUserEventArgs : MVCForumEventArgs
    {
        public MembershipUser User { get; set; }
        public MembershipCreateStatus CreateStatus { get; set; }
    }
}
