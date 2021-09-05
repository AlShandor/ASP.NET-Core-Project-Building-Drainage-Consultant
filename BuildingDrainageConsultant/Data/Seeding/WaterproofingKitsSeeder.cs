namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class WaterproofingKitsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.WaterproofingKits.Any())
            {
                return;
            }

            var atticaPartService = serviceProvider.GetRequiredService<IWaterproofingKitService>();

            var atticaParts = new WaterproofingKit[]
            {
                new WaterproofingKit
                {
                    Name = "HL83.M",
                    ImageId = 1,
                    Description = "Хидроизолационна гарнитура с битумен маншет"
                },
                new WaterproofingKit
                {
                    Name = "HL83.0",
                    ImageId = 1,
                    Description = "Хидроизолационна гарнитура за притискане на тънки пластове фолио"
                },
                new WaterproofingKit
                {
                    Name = "HL83.P",
                    ImageId = 1,
                    Description = "Хидроизолациионна гарнитура за връзка с PVC хидроизолации"
                },
            };

            atticaPartService.CreateAll(atticaParts);
        }
    }
}
