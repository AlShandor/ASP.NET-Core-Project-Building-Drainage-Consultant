namespace BuildingDrainageConsultant.Services.Drains.Models
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System.Collections.Generic;

    public class DrainServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double FlowRate { get; set; }

        public int DrainageArea { get; set; }

        public int Depth { get; set; }

        public DrainDirectionEnum Direction { get; set; }

        public DrainDiameterEnum Diameter { get; set; }

        public DrainVisiblePartEnum VisiblePart { get; set; }

        public DrainWaterproofingEnum Waterproofing { get; set; }

        public DrainHeatingEnum Heating { get; set; }

        public DrainRenovationEnum Renovation { get; set; }

        public DrainFlapSealEnum FlapSeal { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }

        public int? WaterproofingKitId { get; set; }

        public WaterproofingKitServiceModel WaterproofingKit { get; set; }

        public IEnumerable<AccessoryServiceModel> Accessories { get; set; }

    }
}