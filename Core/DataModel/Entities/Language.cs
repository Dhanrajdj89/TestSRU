﻿using System;
using System.Collections.Generic;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Core.DataModel
{
    public partial class Language : Entity
    {
        public Language()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string FlagImageFileName { get; set; }       
        public bool RightToLeft { get; set; }
        public virtual IList<LocaleStringResource> LocaleStringResources { get; set; }
    }
}
