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
        public string Name { get; init; }

        [Required]
        [MaxLength(FlowRateMax)]
        public double FlowRate { get; init; }

        [Required]
        [MaxLength(DraingeAreaMax)]
        public int DrainageArea { get; init; }

        [Required]
        public DrainDiameterEnum Diameter { get; init; }

        [Required]
        public DrainVisiblePartEnum VisiblePart { get; init; }

        [Required]
        public DrainWaterproofingEnum Waterproofing { get; init; }

        [Required]
        public bool HasHeating { get; init; }

        [Required]
        public bool ForRenovation { get; init; }

        [Required]
        public bool HasFlapSeal { get; init; }

        [Url]
        public string ImageUrl { get; set; } = DefaultImageUrl;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
