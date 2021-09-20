﻿namespace BuildingDrainageConsultant.Data.Seeding
{
    using System;
    using System.Collections.Generic;

    public class ApplicationDbContextSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
                          {
                              new ImageSeeder(),
                              new ArticlesSeeder(),
                              new MerchantsSeeder(),
                              new AtticaDetailsSeeder(),
                              new AtticaPartsSeeder(),
                              new AtticaDrainsSeeder(),
                              new WaterproofingKitsSeeder(),
                              new AccessoriesSeeder(),
                              new ExtensionsSeeder(),
                              new DrainsSeeder()
                          };

            foreach (var seeder in seeders)
            {
                seeder.Seed(dbContext, serviceProvider);
                dbContext.SaveChangesAsync();
            }
        }
    }
}
