using System;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Core.DataModel.Activity
{
    public enum ActivityType
    {
        BadgeAwarded,
        MemberJoined,
        ProfileUpdated,
    }

    public class Activity : Entity
    {
        public Activity()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
