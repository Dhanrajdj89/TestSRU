using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class PostMadeEventArgs : MVCForumEventArgs
    {
        public Post Post { get; set; }
    }
}
