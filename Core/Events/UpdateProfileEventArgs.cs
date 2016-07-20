using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class UpdateProfileEventArgs : MVCForumEventArgs
    {
        public MembershipUser User { get; set; }
    }
}
