namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Accessories;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    public class AccessoriesExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Accessories.Any())
            {
                return;
            }

            var accessoryService = serviceProvider.GetRequiredService<IAccessoryService>();

            var fileName = "wwwroot/data/accessoriesData.xlsx";

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

                        var accessoryData = new Accessory
                        {
                            Name = reader.GetValue(0).ToString(),
                            ImageId = accessoryService.GetImageIdByName(reader.GetValue(0).ToString()),
                            Description = reader.GetValue(1).ToString()
                        };

                        dbContext.Accessories.Add(accessoryData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }
}
