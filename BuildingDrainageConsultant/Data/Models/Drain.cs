namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    public class Drain
    {
        [Key]
        public int Id { get; init; }

        [Required]

        public string Name { get; init; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]

        public string Description { get; set; }

        [Required]

        public double FlowRate { get; init; }

        [Required]

        public int DrainageArea { get; init; }

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
    }
}
