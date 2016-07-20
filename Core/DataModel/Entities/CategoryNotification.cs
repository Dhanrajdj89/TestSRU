using System;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Core.DataModel
{
    public partial class CategoryNotification : Entity
    {
        public CategoryNotification()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public virtual Category Category { get; set; }
        public virtual MembershipUser User { get; set; }
    }
}
