namespace BuildingDrainageConsultant.Services.SafeDrains.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using BuildingDrainageConsultant.Services.Images.Models;

    public class SafeDrainServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double FlowRateFree { get; set; }
        public double FlowRate3mVertical { get; set; }
        public int DrainageAreaFree { get; set; }
        public int DrainageArea3mVertical { get; set; }
        public int Depth { get; set; }
        public SafeDrainDirectionEnum Direction { get; set; }
        public SafeDrainDiameterEnum Diameter { get; set; }
        public SafeDrainWaterproofingEnum Waterproofing { get; set; }
        public SafeDrainHeatingEnum Heating { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public ImageHLServiceModel Image { get; set; }
    }
}
