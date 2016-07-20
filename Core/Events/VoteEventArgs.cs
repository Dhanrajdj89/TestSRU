using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class VoteEventArgs  : MVCForumEventArgs
    {
        public Vote Vote { get; set; }
    }
}
