using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTLTestTask.Models;

namespace RTLTestTask.Db.Configurations
{
    public class CastConfiguration : IEntityTypeConfiguration<Cast>
    {
        public void Configure(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");

            builder.HasKey(k => k.Id);
        }
    }
}