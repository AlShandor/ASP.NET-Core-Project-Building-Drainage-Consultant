namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Extensions;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    public class ExtensionsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Extensions.Any())
            {
                return;
            }

            var extensionService = serviceProvider.GetRequiredService<IExtensionService>();

            var fileName = "wwwroot/data/extensionsData.xlsx";

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

                        var extensionData = new Extension
                        {
                            Name = reader.GetValue(0).ToString(),
                            ImageId = extensionService.GetImageIdByName(reader.GetValue(0).ToString()),
                            Description = reader.GetValue(1).ToString()
                        };

                        dbContext.Extensions.Add(extensionData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }
}
