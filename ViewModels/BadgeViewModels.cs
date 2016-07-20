using SportsRUsApp.Core.DataModel;
using System;
using System.Collections.Generic;

namespace SportsRUsApp.Web.ViewModels
{
    public class VoteBadgeViewModel
    {
        public Guid PostId { get; set; }
    }

    public class MarkAsSolutionBadgeViewModel
    {
        public Guid PostId { get; set; }
    }

    public class FavouriteViewModel
    {
        public Guid FavouriteId { get; set; }
    }

    public class PostBadgeViewModel
    {
        public Guid PostId { get; set; }
    }

    public class TimeBadgeViewModel
    {
        public Guid Id { get; set; }
    }

    public class AllBadgesViewModel
    {
        public IList<Badge> AllBadges { get; set; }
    }
}