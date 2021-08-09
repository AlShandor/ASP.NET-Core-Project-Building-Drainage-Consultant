namespace BuildingDrainageConsultant.Models.AtticaDetails
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.AtticaDetail;
    public class AtticaDetailFormModel
    {
        public int Id { get; init; }

        [Required]
        public AtticaRoofTypeEnum RoofType { get; init; }

        [Required]
        public AtticaWalkableEnum IsWalkable { get; init; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; init; }

        public AtticaVisiblePartEnum VisiblePart { get; init; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; init; }

        //TODO
        //public IEnumerable<AtticaDrainServiceModel> AtticaDrains { get; set; } = new List<AtticaDrain>();
    }
}
