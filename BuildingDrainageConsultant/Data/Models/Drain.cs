namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Drain;
    public class Drain
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(FlowRateMax)]
        public double FlowRate { get; set; }

        [Required]
        [MaxLength(DraingeAreaMax)]
        public int DrainageArea { get; set; }

        [Required]
        public DrainDiameterEnum Diameter { get; set; }

        [Required]
        public DrainVisiblePartEnum VisiblePart { get; set; }

        [Required]
        public DrainWaterproofingEnum Waterproofing { get; set; }

        [Required]
        public bool HasHeating { get; set; }

        [Required]
        public bool ForRenovation { get; set; }

        [Required]
        public bool HasFlapSeal { get; set; }

        [Url]
        public string ImageUrl { get; set; } = DefaultImageUrl;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
