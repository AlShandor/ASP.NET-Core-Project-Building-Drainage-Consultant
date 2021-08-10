namespace BuildingDrainageConsultant.Models.AtticaDetails
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.AtticaDetail;
    public class AtticaDetailFormModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Roof Type")]
        public AtticaRoofTypeEnum RoofType { get; set; }

        [Required]
        [Display(Name = "Walkable")]
        public AtticaWalkableEnum IsWalkable { get; set; }

        [Display(Name = "Screed Waterproofing")]
        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        [Display(Name = "Visible Part")]

        public AtticaVisiblePartEnum VisiblePart { get; set; }

        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
