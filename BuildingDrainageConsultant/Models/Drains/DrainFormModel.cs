﻿namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Drain;
    public class DrainFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(0, FlowRateMax)]
        [Display(Name = "Flow Rate (l/s)")]
        public double FlowRate { get; init; }

        [Required]
        [Range(0, DraingeAreaMax)]
        [Display(Name = "Drainage Area (m²)")]
        public int DrainageArea { get; init; }

        [Required]
        [Range(0, DepthMax)]
        [Display(Name = "Depth (mm)")]
        public int Depth { get; set; }

        [Required]
        public DrainDirectionEnum Direction { get; set; }

        [Required]
        public DrainDiameterEnum Diameter { get; init; }

        [Required]
        [Display(Name = "Visible Part")]
        public DrainVisiblePartEnum VisiblePart { get; init; }

        [Required]
        [Display(Name = "Waterproofing Type")]
        public DrainWaterproofingEnum Waterproofing { get; init; }

        [Required]
        public DrainHeatingEnum Heating { get; init; }

        [Required]
        [Display(Name = "For Renovation")]
        public DrainRenovationEnum Renovation { get; init; }

        [Required]
        [Display(Name = "Flap Seal")]
        public DrainFlapSealEnum FlapSeal { get; init; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; init; }
    }
}