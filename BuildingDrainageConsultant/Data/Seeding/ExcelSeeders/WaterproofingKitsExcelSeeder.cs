namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

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

                        var waterproofingKitData = new WaterproofingKit
                        {
                            Name = reader.GetValue(0).ToString(),
                            ImageId = waterproofingKitService.GetImageIdByName(reader.GetValue(0).ToString()),
                            Description = reader.GetValue(1).ToString()
                        };

                        dbContext.WaterproofingKits.Add(waterproofingKitData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }
}
