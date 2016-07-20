using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Web.Areas.Admin.ViewModels
{
    public class ListBadgesViewModel
    {
        public PagedList<Badge> Badges { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public string Search { get; set; }
    }

}