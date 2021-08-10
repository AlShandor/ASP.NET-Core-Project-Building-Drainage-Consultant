namespace BuildingDrainageConsultant.Models.AtticaParts
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.AtticaPart;
    public class AtticaPartFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
