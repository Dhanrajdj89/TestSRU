﻿using System;
using System.Collections.Generic;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Core.DataModel
{
    public partial class PollAnswer
    {
        public PollAnswer()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual IList<PollVote> PollVotes { get; set; }
    }
}
