namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Drain;
    public class Drain
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(FlowRateMin, FlowRateMax)]
        public double FlowRate { get; set; }

        [Required]
        [Range(DraingeAreaMin, DraingeAreaMax)]
        public int DrainageArea { get; set; }

        [Required]
        [Range(DepthMin, DepthMax)]
        public int Depth { get; set; }

        [Required]
        public DrainDirectionEnum Direction { get; set; }

        [Required]
        public DrainDiameterEnum Diameter { get; set; }

        [Required]
        public DrainVisiblePartEnum VisiblePart { get; set; }

        [Required]
        public DrainWaterproofingEnum Waterproofing { get; set; }

        [Required]
        public DrainHeatingEnum Heating { get; set; }

        [Required]
        public DrainRenovationEnum Renovation { get; set; }

        [Required]
        public DrainFlapSealEnum FlapSeal { get; set; }

        [Required]
        public DrainLoadClassEnum LoadClass { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }

        public int? WaterproofingKitId { get; set; }

        public WaterproofingKit WaterproofingKit { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public virtual ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();

        public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();
    }
}
