namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Seeding.ExcelSeeders;
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
                              new AtticaDetailsExcelSeeder(),
                              new AtticaPartsExcelSeeder(),
                              new AtticaDrainsSeeder(),
                              new WaterproofingKitsExcelSeeder(),
                              new AccessoriesExcelSeeder(),
                              new ExtensionsExcelSeeder(),
                              new DrainsExcelSeeder()
                          };

            foreach (var seeder in seeders)
            {
                seeder.Seed(dbContext, serviceProvider);
                dbContext.SaveChangesAsync();
            }
        }
    }
}
