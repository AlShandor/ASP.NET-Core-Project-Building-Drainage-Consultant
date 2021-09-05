namespace BuildingDrainageConsultant.Data.Seeding
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
                              new AtticaDetailsSeeder(),
                              new AtticaPartsSeeder(),
                              new AtticaDrainsSeeder(),
                              new DrainsSeeder(),
                              new MerchantsSeeder(),
                              new WaterproofingKitsSeeder(),
                              new AccessoriesSeeder()
                          };

            foreach (var seeder in seeders)
            {
                seeder.Seed(dbContext, serviceProvider);
                dbContext.SaveChangesAsync();
            }
        }
    }
}
