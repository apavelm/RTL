using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTLTestTask.Models;

namespace RTLTestTask.Db.Configurations
{
    public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
    {
        public void Configure(EntityTypeBuilder<TVShow> builder)
        {
            builder.ToTable("Show");

            builder.HasKey(k => k.Id);
        }
    }
}
