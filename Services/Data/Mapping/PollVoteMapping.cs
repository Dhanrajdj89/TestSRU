using System.Data.Entity.ModelConfiguration;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Services.Data.Mapping
{
    public class PollVoteMapping : EntityTypeConfiguration<PollVote>
    {
        public PollVoteMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
        }
    }
}
