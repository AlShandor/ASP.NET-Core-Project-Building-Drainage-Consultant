namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Services.Drains;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static DataConstants.ExcelSeeding;
    public class DrainsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Drains.Any())
            {
                return;
            }

            var drainService = serviceProvider.GetRequiredService<IDrainService>();

            var fileName = "wwwroot/data/drainsData.xlsx";

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
                        var flowRate = reader.GetValue(FlowRateColumn) == null ? 0 : double.Parse(reader.GetValue(FlowRateColumn).ToString().Trim());
                        var drainageArea = reader.GetValue(DrainageAreaColumn) == null ? 0 : int.Parse(reader.GetValue(DrainageAreaColumn).ToString().Trim());
                        var depth = reader.GetValue(DepthColumn) == null ? 0 : int.Parse(reader.GetValue(DepthColumn).ToString().Trim());

                        DrainDirectionEnum direction;
                        Enum.TryParse(reader.GetValue(DirectionColumn).ToString().Trim(), out direction);

                        DrainDiameterEnum diameter;
                        Enum.TryParse(reader.GetValue(DiameterColumn).ToString().Trim(), out diameter);

                        DrainVisiblePartEnum visiblePart;
                        Enum.TryParse(reader.GetValue(VisiblePartColumn).ToString().Trim(), out visiblePart);

                        DrainWaterproofingEnum waterproofing;
                        Enum.TryParse(reader.GetValue(WaterproofingColumn).ToString().Trim(), out waterproofing);

                        DrainHeatingEnum heating;
                        Enum.TryParse(reader.GetValue(HeatingColumn).ToString().Trim(), out heating);

                        DrainRenovationEnum renovation;
                        Enum.TryParse(reader.GetValue(RenovationColumn).ToString().Trim(), out renovation);

                        DrainFlapSealEnum flapSeal;
                        Enum.TryParse(reader.GetValue(FlapSealColumn).ToString().Trim(), out flapSeal);

                        DrainLoadClassEnum loadClass;
                        Enum.TryParse(reader.GetValue(LoadClassColumn).ToString().Trim(), out loadClass);

                        var imageName = reader.GetValue(ImageColumn) == null ? string.Empty : reader.GetValue(ImageColumn).ToString().Trim();

                        var waterproofingKitName = reader.GetValue(WaterproofingKitColumn) == null ? 
                            string.Empty : 
                            reader.GetValue(WaterproofingKitColumn).ToString().Trim();

                        var accessories = reader.GetValue(AccessoriesColumn) == null ? 
                            string.Empty : 
                            reader.GetValue(AccessoriesColumn).ToString().Trim();

                        var drainData = new Drain
                        {
                            Name = name,
                            Description = description,
                            FlowRate = flowRate,
                            DrainageArea = drainageArea,
                            Depth = depth,
                            Direction = direction,
                            Diameter = diameter,
                            VisiblePart = visiblePart,
                            Waterproofing = waterproofing,
                            Heating = heating,
                            Renovation = renovation,
                            FlapSeal = flapSeal,
                            LoadClass = loadClass,
                            ImageId = drainService.GetImageIdByName(imageName),
                            WaterproofingKitId = drainService.GetWaterproofingKitId(waterproofingKitName),
                            Accessories = drainService.GetAccessoriesFromString(accessories)
                        };

                        dbContext.Drains.Add(drainData);
                    }
                }
            }

            dbContext.SaveChanges();
        }

    }
}
