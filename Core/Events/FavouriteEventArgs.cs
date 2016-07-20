using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Events
{
    public class FavouriteEventArgs : MVCForumEventArgs
    {
        public Favourite Favourite { get; set; }
    }
}
