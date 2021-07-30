namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Drain;
    public class DrainFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(0, FlowRateMax)]
        [Display(Name = "Flow Rate (l/s)")]
        public double FlowRate { get; init; }

        [Required]
        [Range(0, DraingeAreaMax)]
        [Display(Name = "Drainage Area (m²)")]
        public int DrainageArea { get; init; }

        [Required]
        public DrainDiameterEnum Diameter { get; init; }

        [Required]
        [Display(Name = "Visible Part")]
        public DrainVisiblePartEnum VisiblePart { get; init; }

        [Required]
        [Display(Name = "Waterproofing Type")]
        public DrainWaterproofingEnum Waterproofing { get; init; }

        [Required]
        [Display(Name = "Heating")]
        public bool HasHeating { get; init; }

        [Required]
        [Display(Name = "For Renovation")]
        public bool ForRenovation { get; init; }

        [Required]
        [Display(Name = "Flap Seal")]
        public bool HasFlapSeal { get; init; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; init; }
    }
}
