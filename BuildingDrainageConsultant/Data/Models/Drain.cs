﻿namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
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
        [MaxLength(FlowRateMax)]
        public double FlowRate { get; set; }

        [Required]
        [MaxLength(DraingeAreaMax)]
        public int DrainageArea { get; set; }

        [Required]
        [MaxLength(DepthMax)]
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

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
