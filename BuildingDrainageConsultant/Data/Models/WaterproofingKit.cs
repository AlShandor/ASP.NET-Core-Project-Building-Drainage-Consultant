namespace BuildingDrainageConsultant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.WaterproofingKit;
    public class WaterproofingKit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }

        public ICollection<Drain> Drains { get; set; } = new List<Drain>();
    }
}
