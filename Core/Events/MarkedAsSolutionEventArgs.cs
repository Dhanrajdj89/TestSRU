using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class MarkedAsSolutionEventArgs : MVCForumEventArgs
    {
        public Topic Topic { get; set; }
        public Post Post { get; set; }
        public MembershipUser Marker { get; set; }
        public MembershipUser SolutionWriter { get; set; }
    }
}
