namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static DataConstants.ExcelSeeding;
    public class AtticaDetailsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AtticaDetails.Any())
            {
                return;
            }

            var atticaDetailService = serviceProvider.GetRequiredService<IAtticaDetailService>();

            var fileName = "wwwroot/data/atticaDetailsData.xlsx";

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

                        var atticaDetailImageName = reader.GetValue(NameColumn).ToString().Trim();

                        AtticaRoofTypeEnum roofType;
                        Enum.TryParse(reader.GetValue(RoofTypeColumn).ToString().Trim(), out roofType);

                        AtticaWalkableEnum isWalkable;
                        Enum.TryParse(reader.GetValue(WalkableColumn).ToString().Trim(), out isWalkable);

                        AtticaScreedWaterproofingEnum screedWaterproofing;
                        var screedWaterproofingStr = reader.GetValue(ScreedWaterproofingColumn) == null ?
                            string.Empty :
                            reader.GetValue(ScreedWaterproofingColumn).ToString().Trim();
                        Enum.TryParse(screedWaterproofingStr, out screedWaterproofing);

                        var atticaDetailData = new AtticaDetail
                        {
                            Description = reader.GetValue(DescriptionColumn).ToString(),
                            RoofType = roofType,
                            IsWalkable = isWalkable,
                            ScreedWaterproofing = screedWaterproofing,
                            ImageId = atticaDetailService.GetImageIdByName(atticaDetailImageName)
                        };

                        dbContext.AtticaDetails.Add(atticaDetailData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }
}
