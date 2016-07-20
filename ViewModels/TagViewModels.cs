using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Web.ViewModels
{
    public class PopularTagViewModel
    {
        public Dictionary<TopicTag, int> PopularTags { get; set; }
    }
}