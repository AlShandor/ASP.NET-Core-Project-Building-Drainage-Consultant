namespace BuildingDrainageConsultant.Models.SafeDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.SafeDrain;
    public class SafeDrainFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(FlowRateMin, FlowRateMax)]
        [Display(Name = "Flow Rate (l/s) Free Flow")]
        public double FlowRateFree { get; set; }

        [Required]
        [Range(FlowRateMin, FlowRateMax)]
        [Display(Name = "Flow Rate (l/s)  ")]
        public double FlowRate3mVertical { get; set; }

        [Required]
        [Range(DraingeAreaMin, DraingeAreaMax)]
        [Display(Name = "Drainage Area (m²) Free Flow")]
        public int DrainageAreaFree { get; set; }

        [Required]
        [Range(DraingeAreaMin, DraingeAreaMax)]
        [Display(Name = "Drainage Area (m²) with 3 m Vertical Branch")]

        public int DrainageArea3mVertical { get; set; }

        [Required]
        [Range(DepthMin, DepthMax)]
        [Display(Name = "Depth (mm)")]
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

        public bool IsMyDrain { get; set; }

        public int? ImageId { get; set; }

        public ImageHLServiceModel Image { get; set; }

        public IEnumerable<ImageHLServiceModel> Images { get; set; }
    }
}
