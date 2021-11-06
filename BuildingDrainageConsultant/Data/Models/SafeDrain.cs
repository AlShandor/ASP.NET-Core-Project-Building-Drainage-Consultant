namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.SafeDrain;
    public class SafeDrain
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(FlowRateMin, FlowRateMax)]
        public double FlowRateFree { get; set; }

        [Required]
        [Range(FlowRateMin, FlowRateMax)]
        public double FlowRate3mVertical { get; set; }

        [Required]
        [Range(DraingeAreaMin, DraingeAreaMax)]
        public int DrainageAreaFree { get; set; }

        [Required]
        [Range(DraingeAreaMin, DraingeAreaMax)]
        public int DrainageArea3mVertical { get; set; }

        [Required]
        [Range(DepthMin, DepthMax)]
        public int Depth { get; set; }

        [Required]
        public SafeDrainDirectionEnum Direction { get; set; }

        [Required]
        public SafeDrainDiameterEnum Diameter { get; set; }

        [Required]
        public SafeDrainWaterproofingEnum Waterproofing { get; set; }

        [Required]
        public SafeDrainHeatingEnum Heating { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
