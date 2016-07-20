using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class PrivateMessageEventArgs : MVCForumEventArgs
    {
        public PrivateMessage PrivateMessage { get; set; }
    }
}
