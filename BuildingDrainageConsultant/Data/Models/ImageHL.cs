namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Image;
    public class ImageHL
    {
        [Key]
        public int Id { get; set; }

        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [StringLength(PathMaxLength)]
        public string Path { get; set; }

        public ImageHLCategoriesEnum ImageCategory { get; set; }
    }
}
