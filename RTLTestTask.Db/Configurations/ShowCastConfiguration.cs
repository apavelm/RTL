using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTLTestTask.Models;

namespace RTLTestTask.Db.Configurations
{
    public class ShowCastConfiguration : IEntityTypeConfiguration<ShowCast>
    {
        public void Configure(EntityTypeBuilder<ShowCast> builder)
        {
            builder.ToTable("ShowCast");
        }
    }
}