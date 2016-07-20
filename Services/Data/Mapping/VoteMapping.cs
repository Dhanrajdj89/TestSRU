using System.Data.Entity.ModelConfiguration;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Services.Data.Mapping
{
    public class VoteMapping : EntityTypeConfiguration<Vote>
    {
        public VoteMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Amount).IsRequired();
            Property(x => x.DateVoted).IsOptional();
        }
    }
}
