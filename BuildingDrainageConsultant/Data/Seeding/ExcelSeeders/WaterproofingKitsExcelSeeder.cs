namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static ExcelSeedingConstants.Drains;

    public class WaterproofingKitsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.WaterproofingKits.Any())
            {
                return;
            }

            var waterproofingKitService = serviceProvider.GetRequiredService<IWaterproofingKitService>();

            var fileName = "wwwroot/data/waterproofingKitsData.xlsx";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        if (reader.Depth == 0) // skip Headers
                        {
                            continue;
                        }

                        var name = reader.GetValue(NameColumn) == null ? string.Empty : reader.GetValue(NameColumn).ToString().Trim();
                        var description = reader.GetValue(DescriptionColumn) == null ? string.Empty : reader.GetValue(DescriptionColumn).ToString().Trim();

                        var waterproofingKitData = new WaterproofingKit
                        {
                            Name = name,
                            Description = description,
                            ImageId = waterproofingKitService.GetImageIdByName(name)
                        };

                        dbContext.WaterproofingKits.Add(waterproofingKitData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }
}
