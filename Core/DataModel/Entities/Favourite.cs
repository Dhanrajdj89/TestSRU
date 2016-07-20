﻿using System;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Core.DataModel
{
    public partial class Favourite : Entity
    {
        public Favourite()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual MembershipUser Member { get; set; }
        public virtual Post Post { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
