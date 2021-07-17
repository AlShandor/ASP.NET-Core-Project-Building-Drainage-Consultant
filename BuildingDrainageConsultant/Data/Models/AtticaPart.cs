namespace BuildingDrainageConsultant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.AtticaPart;
    public class AtticaPart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Url]
        public string ImageUrl { get; set; } = DefaultImageUrl;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; init; }
    }
}
