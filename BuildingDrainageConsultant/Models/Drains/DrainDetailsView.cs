namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums;

    public class DrainDetailsView
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public double FlowRate { get; init; }

        public int DrainageArea { get; init; }

        public DrainDiameterEnum Diameter { get; init; }

        public DrainVisiblePartEnum VisiblePart { get; init; }

        public DrainWaterproofingEnum Waterproofing { get; init; }

        public bool HasHeating { get; init; }

        public bool ForRenovation { get; init; }

        public bool HasFlapSeal { get; init; }

        public string ImageUrl { get; init; }

        public string Description { get; init; }
    }
}
