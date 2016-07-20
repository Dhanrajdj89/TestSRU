using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class BadgeEventArgs : MVCForumEventArgs
    {
        public MembershipUser User { get; set; }
        public BadgeType BadgeType { get; set; }

    }
}
