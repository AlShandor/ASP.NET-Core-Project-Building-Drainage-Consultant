namespace BuildingDrainageConsultant.Services.Drains.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;

    public class DrainServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public double FlowRate { get; init; }

        public int DrainageArea { get; init; }

        public int Depth { get; init; }

        public DrainDirectionEnum Direction { get; init; }

        public DrainDiameterEnum Diameter { get; init; }

        public DrainVisiblePartEnum VisiblePart { get; init; }

        public DrainWaterproofingEnum Waterproofing { get; init; }

        public DrainHeatingEnum Heating { get; init; }

        public DrainRenovationEnum Renovation { get; init; }

        public DrainFlapSealEnum FlapSeal { get; init; }

        public string ImageUrl { get; init; }

        public string Description { get; init; }
    }
}
