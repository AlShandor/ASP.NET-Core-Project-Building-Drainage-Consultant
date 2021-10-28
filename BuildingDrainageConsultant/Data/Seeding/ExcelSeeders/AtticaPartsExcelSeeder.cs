namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static ExcelSeedingConstants.Attica;

    public class AtticaPartsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AtticaParts.Any())
            {
                return;
            }

            var atticaPartService = serviceProvider.GetRequiredService<IAtticaPartService>();

            var fileName = "wwwroot/data/atticaPartsData.xlsx";

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

                        var partData = new AtticaPart
                        {
                            Name = name,
                            Description = description,
                            ImageId = atticaPartService.GetImageIdByName(reader.GetValue(AtticaPartImageNameColumn).ToString().Trim())
                        };

                        dbContext.AtticaParts.Add(partData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }

}
