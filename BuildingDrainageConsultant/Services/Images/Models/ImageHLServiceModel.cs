namespace BuildingDrainageConsultant.Services.Images.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Image;
    public class ImageHLServiceModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PathMaxLength)]
        public string Path { get; set; }

        public ImageHLCategoriesEnum ImageCategory { get; set; }
    }
}
