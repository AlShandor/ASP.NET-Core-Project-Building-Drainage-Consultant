
namespace BuildingDrainageConsultant.Models.AtticaDrains
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.AtticaPart;
    public class AtticaPartFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; init; }
    }
}
