﻿using System.Data.Entity.ModelConfiguration;
using SportsRUsApp.Core.DataModel.Activity;

namespace SportsRUsApp.Services.Data.Mapping
{
    public class ActivityMapping : EntityTypeConfiguration<Activity>
    {
        public ActivityMapping()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.Timestamp).IsRequired();
            Property(x => x.Data).IsRequired();
            Property(x => x.Type).IsRequired().HasMaxLength(50);

            // TODO - Change Table Names
            //ToTable("Activity"); 
        }
    }
}
