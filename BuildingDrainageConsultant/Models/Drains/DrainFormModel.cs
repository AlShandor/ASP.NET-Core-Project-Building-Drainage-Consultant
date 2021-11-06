namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using BuildingDrainageConsultant.Services.Images.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Drain;
    public class DrainFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(FlowRateMin, FlowRateMax)]
        [Display(Name = "Flow Rate (l/s)")]
        public double FlowRate { get; set; }

        [Required]
        [Range(DraingeAreaMin, DraingeAreaMax)]
        [Display(Name = "Drainage Area (m²)")]
        public int DrainageArea { get; set; }

        [Required]
        [Range(DepthMin, DepthMax)]
        [Display(Name = "Depth (mm)")]
        public int Depth { get; set; }

        [Required]
        public DrainDirectionEnum Direction { get; set; }

        [Required]
        public DrainDiameterEnum Diameter { get; set; }

        [Required]
        [Display(Name = "Visible Part")]
        public DrainVisiblePartEnum VisiblePart { get; set; }

        [Required]
        [Display(Name = "Waterproofing Type")]
        public DrainWaterproofingEnum Waterproofing { get; set; }

        [Required]
        public DrainHeatingEnum Heating { get; set; }

        [Required]
        [Display(Name = "For Renovation")]
        public DrainRenovationEnum Renovation { get; set; }

        [Required]
        [Display(Name = "Flap Seal")]
        public DrainFlapSealEnum FlapSeal { get; set; }

        [Required]
        [Display(Name = "Load Class")]
        public DrainLoadClassEnum LoadClass { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int WaterproofingKitId { get; set; }

        public int AccessoryId { get; set; }

        public int ExtensionId { get; set; }

        public WaterproofingKitServiceModel WaterproofingKit { get; set; }

        public bool IsMyDrain { get; set; }

        public int? ImageId { get; set; }

        public ImageHLServiceModel Image { get; set; }

        public IEnumerable<ImageHLServiceModel> Images { get; set; }

        public ICollection<WaterproofingKitServiceModel> WaterproofingKits { get; set; }

        public ICollection<AccessoryServiceModel> Accessories { get; set; }

        public ICollection<ExtensionServiceModel> Extensions { get; set; }

    }
}