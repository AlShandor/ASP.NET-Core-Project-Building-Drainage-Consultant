namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using BuildingDrainageConsultant.Services.SafeDrains;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static ExcelSeedingConstants.SafeDrains;

    public class SafeDrainsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SafeDrains.Any())
            {
                return;
            }

            var safeDrainService = serviceProvider.GetRequiredService<ISafeDrainService>();

            var fileName = "wwwroot/data/safeDrainsData.xlsx";

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
                        var flowRateFree = reader.GetValue(FlowRateFreeColumn) == null ? 0 : double.Parse(reader.GetValue(FlowRateFreeColumn).ToString().Trim());
                        var flowRate3mVertical = reader.GetValue(FlowRate3mVerticalColumn) == null ? 0 : double.Parse(reader.GetValue(FlowRate3mVerticalColumn).ToString().Trim());
                        var drainageAreaFree = reader.GetValue(DrainageAreaFreeColumn) == null ? 0 : int.Parse(reader.GetValue(DrainageAreaFreeColumn).ToString().Trim());
                        var drainageArea3mVertical = reader.GetValue(DrainageArea3mVerticalColumn) == null ? 0 : int.Parse(reader.GetValue(DrainageArea3mVerticalColumn).ToString().Trim());
                        var depth = reader.GetValue(DepthColumn) == null ? 0 : int.Parse(reader.GetValue(DepthColumn).ToString().Trim());
                        var imageName = reader.GetValue(ImageColumn) == null ? string.Empty : reader.GetValue(ImageColumn).ToString().Trim();

                        SafeDrainDirectionEnum direction;
                        Enum.TryParse(reader.GetValue(DirectionColumn).ToString().Trim(), out direction);

                        SafeDrainDiameterEnum diameter;
                        Enum.TryParse(reader.GetValue(DiameterColumn).ToString().Trim(), out diameter);

                        SafeDrainWaterproofingEnum waterproofing;
                        Enum.TryParse(reader.GetValue(WaterproofingColumn).ToString().Trim(), out waterproofing);

                        SafeDrainHeatingEnum heating;
                        Enum.TryParse(reader.GetValue(HeatingColumn).ToString().Trim(), out heating);

                        var safeDrainData = new SafeDrain
                        {
                            Name = name,
                            Description = description,
                            FlowRateFree = flowRateFree,
                            FlowRate3mVertical = flowRate3mVertical,
                            DrainageAreaFree = drainageAreaFree,
                            DrainageArea3mVertical = drainageArea3mVertical,
                            Depth = depth,
                            Direction = direction,
                            Diameter = diameter,
                            Waterproofing = waterproofing,
                            Heating = heating,
                            ImageId = safeDrainService.GetImageIdByName(imageName)
                        };

                        dbContext.SafeDrains.Add(safeDrainData);
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }
}
