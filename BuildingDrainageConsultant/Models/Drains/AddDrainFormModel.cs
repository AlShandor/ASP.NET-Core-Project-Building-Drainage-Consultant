namespace BuildingDrainageConsultant.Models.Drains
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Enums;

    using static Data.DataConstants.Drain;
    public class AddDrainFormModel
    {
        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(0, FlowRateMax)]
        [Display(Name = "Flow Rate")]
        public double FlowRate { get; init; }

        [Required]
        [Range(0, DraingeAreaMax)]
        [Display(Name = "Drainage Area")]
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
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

    }
}
