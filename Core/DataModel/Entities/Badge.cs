using System;
using System.Collections.Generic;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Core.DataModel
{
    public enum BadgeType
    {
        VoteUp,
        VoteDown,
        MarkAsSolution,
        Time,
        Post,
        Profile,
        Favourite,
        Tag
    }

    public partial class Badge : Entity
    {
        public Badge()
        {
            Id = GuidComb.GenerateComb();
        }

        /// <summary>
        /// Specifies the target badge interface names matched to the corresponding badge type
        /// </summary>
        public static readonly Dictionary<BadgeType, string> BadgeClassNames = new Dictionary<BadgeType, string>
                                                            {
                                                                {BadgeType.VoteUp, "SportsRUsApp.Core.Interfaces.Badges.IVoteUpBadge"},
                                                                {BadgeType.MarkAsSolution, "SportsRUsApp.Core.Interfaces.Badges.IMarkAsSolutionBadge"},
                                                                {BadgeType.Time, "SportsRUsApp.Core.Interfaces.Badges.ITimeBadge"},
                                                                {BadgeType.Post, "SportsRUsApp.Core.Interfaces.Badges.IPostBadge"},
                                                                {BadgeType.VoteDown, "SportsRUsApp.Core.Interfaces.Badges.IVoteDownBadge"},
                                                                {BadgeType.Profile, "SportsRUsApp.Core.Interfaces.Badges.IProfileBadge"},
                                                                {BadgeType.Favourite, "SportsRUsApp.Core.Interfaces.Badges.IFavouriteBadge"},
                                                                {BadgeType.Tag, "SportsRUsApp.Core.Interfaces.Badges.ITagBadge"}
                                                            };

        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Milestone { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? AwardsPoints { get; set; }
        public virtual IList<MembershipUser> Users { get; set; }

    }
}
