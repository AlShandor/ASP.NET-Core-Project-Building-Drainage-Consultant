namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static ExcelSeedingConstants.Attica;

    public class AtticaDrainsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AtticaDrains.Any())
            {
                return;
            }

            var atticaDrainService = serviceProvider.GetRequiredService<IAtticaDrainService>();

            var fileName = "wwwroot/data/atticaDrainsData.xlsx";

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
                        var flowRate35mm = reader.GetValue(FlowRate35mmColumn) == null ? 0 : double.Parse(reader.GetValue(FlowRate35mmColumn).ToString().Trim());
                        var flowRate100mm = reader.GetValue(FlowRate100mmColumn) == null ? 0 : double.Parse(reader.GetValue(FlowRate100mmColumn).ToString().Trim());
                        var drainageArea35mm = reader.GetValue(DrainageArea35mmColumn) == null ? 0 : int.Parse(reader.GetValue(DrainageArea35mmColumn).ToString().Trim());
                        var drainageArea100mm = reader.GetValue(DrainageArea100mmColumn) == null ? 0 : int.Parse(reader.GetValue(DrainageArea100mmColumn).ToString().Trim());
                        int? atticaDetailId = reader.GetValue(AtticaDetailIdColumn) == null ? null : int.Parse(reader.GetValue(AtticaDetailIdColumn).ToString().Trim());
                        var atticaParts = reader.GetValue(AtticaPartsColumn) == null ? string.Empty : reader.GetValue(AtticaPartsColumn).ToString().Trim();

                        AtticaScreedWaterproofingEnum screedWaterproofing;
                        Enum.TryParse(reader.GetValue(ScreedWaterproofingColumn).ToString().Trim(), out screedWaterproofing);

                        AtticaConcreteWaterproofingEnum concreteWaterproofing;
                        Enum.TryParse(reader.GetValue(ConcreteWaterproofingColumn).ToString().Trim(), out concreteWaterproofing);

                        AtticaDiameterEnum diameter;
                        Enum.TryParse(reader.GetValue(DiameterColumn).ToString().Trim(), out diameter);

                        AtticaVisiblePartEnum visiblePart;
                        Enum.TryParse(reader.GetValue(VisiblePartColumn).ToString().Trim(), out visiblePart);

                        var atticaDrainData = new AtticaDrain
                        {
                            Name = name,
                            FlowRate35mm = flowRate35mm,
                            FlowRate100mm = flowRate100mm,
                            DrainageArea35mm = drainageArea35mm,
                            DrainageArea100mm = drainageArea100mm,
                            ScreedWaterproofing = screedWaterproofing,
                            ConcreteWaterproofing = concreteWaterproofing,
                            Diameter = diameter,
                            VisiblePart = visiblePart,
                            AtticaParts = atticaDrainService.GetAtticaPartsFromString(atticaParts),
                            AtticaDetailId = atticaDetailId
                        };

                        dbContext.AtticaDrains.Add(atticaDrainData);
                    }
                }
            }
            dbContext.SaveChanges();
        }
    }
}
