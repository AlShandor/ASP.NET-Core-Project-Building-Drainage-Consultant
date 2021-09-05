namespace BuildingDrainageConsultant.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class BuildingDrainageConsultantDbContext : IdentityDbContext<User>
    {
        public BuildingDrainageConsultantDbContext(DbContextOptions<BuildingDrainageConsultantDbContext> options)
            : base(options)
        {
        }
        public DbSet<Drain> Drains { get; set; }
        public DbSet<AtticaDrain> AtticaDrains { get; set; }
        public DbSet<AtticaDetail> AtticaDetails { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<AtticaPart> AtticaParts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ImageHL> Images { get; set; }
        public DbSet<WaterproofingKit> WaterproofingKits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ImageHLModelCreating(builder);

            builder
                .Entity<WaterproofingKit>()
                .HasMany(i => i.Drains)
                .WithOne(d => d.WaterproofingKit)
                .HasForeignKey(d => d.WaterproofingKitId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<AtticaDrain>()
                .HasOne(a => a.AtticaDetail)
                .WithMany(a => a.AtticaDrains)
                .HasForeignKey(a => a.AtticaDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        private static void ImageHLModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ImageHL>()
                .HasMany(i => i.Articles)
                .WithOne(d => d.Image)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<ImageHL>()
                .HasMany(i => i.AtticaParts)
                .WithOne(d => d.Image)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<ImageHL>()
                .HasMany(i => i.Drains)
                .WithOne(d => d.Image)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<ImageHL>()
                .HasMany(i => i.AtticaDetails)
                .WithOne(d => d.Image)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}