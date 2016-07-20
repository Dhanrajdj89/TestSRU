using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;
using RssItem = SportsRUsApp.Utilities.RssItem;


namespace SportsRUsApp.Web.Areas.Admin.ViewModels
{
    public class MainDashboardNavViewModel
    {
        public int PrivateMessageCount { get; set; }
        public int ModerateCount { get; set; }
    }

    public class LatestUsersViewModels
    {
        public IList<MembershipUser> Users { get; set; }
    }

    public class LowestPointUsersViewModels
    {
        public Dictionary<MembershipUser, int> Users { get; set; }
    }

    public class LowestPointPostsViewModels
    {
        public IList<Post> Posts { get; set; }
    }

    public class HighestViewedTopics
    {
        public IList<Topic> Topics { get; set; }
    }

    public class LatestNewsViewModel
    {
        public IList<RssItem> RssFeed { get; set; }
    }

    public class TodaysTopics
    {
        public IList<Topic> Topics { get; set; }
    }
}