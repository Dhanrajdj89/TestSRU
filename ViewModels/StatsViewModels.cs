using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Web.ViewModels
{
    public class MainStatsViewModel
    {
        public int PostCount { get; set; }
        public int TopicCount { get; set; }
        public int MemberCount { get; set; }
        public IList<MembershipUser> LatestMembers { get; set; }
    }
}