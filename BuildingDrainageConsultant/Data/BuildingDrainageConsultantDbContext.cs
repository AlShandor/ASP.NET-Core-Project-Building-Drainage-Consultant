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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Drain>()
                .HasOne(d => d.User)
                .WithMany(d => d.Drains)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<AtticaDrain>()
                .HasOne(d => d.User)
                .WithMany(d => d.AtticaDrains)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<AtticaDrain>()
                .HasOne(a => a.AtticaDetail)
                .WithMany(a => a.AtticaDrains)
                .HasForeignKey(a => a.AtticaDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}