namespace BuildingDrainageConsultant.Services.Images.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Image;
    public class ImageHLServiceModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(PathMaxLength)]
        public string Path { get; set; }
    }
}
