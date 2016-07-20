using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class TopicMadeEventArgs : MVCForumEventArgs
    {
        public Topic Topic { get; set; }
    }
}
