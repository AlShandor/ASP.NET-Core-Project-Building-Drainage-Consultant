namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Drain;
    public class DrainFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(0, FlowRateMax)]
        [Display(Name = "Flow Rate (l/s)")]
        public double FlowRate { get; set; }

        [Required]
        [Range(0, DraingeAreaMax)]
        [Display(Name = "Drainage Area (m²)")]
        public int DrainageArea { get; set; }

        [Required]
        [Range(0, DepthMax)]
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

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
