using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.DataModel.Activity;
using SportsRUsApp.ViewModels.Application;

namespace SportsRUsApp.Web.ViewModels
{
    public class ListCategoriesViewModels
    {
        public MembershipUser MembershipUser { get; set; }
        public MembershipRole MembershipRole { get; set; }
    }

    public class AllRecentActivitiesViewModel
    {
        public PagedList<ActivityBase> Activities { get; set; }

        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
    }

    public class TermsAndConditionsViewModel
    {
        public string TermsAndConditions { get; set; }

        [ForumMvcResourceDisplayName("TermsAndConditions.Label.Agree")]
        [MustBeTrue(ErrorMessage = "TermsAndConditions.Label.AgreeError")]
        public bool Agree { get; set; }
    }
}