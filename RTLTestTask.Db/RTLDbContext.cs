using Microsoft.EntityFrameworkCore;
using RTLTestTask.Db.Configurations;
using RTLTestTask.Models;

namespace RTLTestTask.Db
{
    public class RTLDbContext: DbContext
    {
        public RTLDbContext() {}

        public RTLDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Cast> Actors { get; set; }
        public virtual DbSet<TVShow> TVShows { get; set; }
        public virtual DbSet<ShowCast> TVShowCast { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShowCast>()
                .HasKey(bc => new { bc.ShowId, bc.CastId });

            modelBuilder.Entity<TVShow>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Cast>()
                .HasKey(k => k.Id);


            modelBuilder.Entity<ShowCast>()
                .HasOne(bc => bc.Cast)
                .WithMany(b => b.ShowCasts)
                .HasForeignKey(bc => bc.CastId);

            modelBuilder.Entity<ShowCast>()
                .HasOne(bc => bc.Show)
                .WithMany(c => c.ShowCasts)
                .HasForeignKey(bc => bc.ShowId);

            modelBuilder.ApplyConfiguration(new CastConfiguration());
            modelBuilder.ApplyConfiguration(new TVShowConfiguration());
            modelBuilder.ApplyConfiguration(new ShowCastConfiguration());
        }
    }
}
