﻿namespace BuildingDrainageConsultant.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class BuildingDrainageConsultantDbContext : IdentityDbContext
    {
        public BuildingDrainageConsultantDbContext(DbContextOptions<BuildingDrainageConsultantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drain> Drains { get; set; }
        public DbSet<AtticaDrain> AtticaDrains { get; set; }
        public DbSet<DrainageDetail> DrainageDetails { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<AtticaPart> AtticaParts { get; set; }
    }
}
