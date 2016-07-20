using System.Data.Entity.ModelConfiguration;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Services.Data.Mapping
{
    public class BannedWordMapping : EntityTypeConfiguration<BannedWord>
    {
        public BannedWordMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Word).IsRequired().HasMaxLength(75);
            Property(x => x.DateAdded).IsRequired();
            Property(x => x.IsStopWord).IsOptional();
        }
    }
}
