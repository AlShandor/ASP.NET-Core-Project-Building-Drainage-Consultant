namespace BuildingDrainageConsultant.Services.WaterproofingKits.Models
{
    using BuildingDrainageConsultant.Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.WaterproofingKit;
    public class WaterproofingKitServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}
