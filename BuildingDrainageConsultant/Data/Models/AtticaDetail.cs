namespace BuildingDrainageConsultant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;

    using static DataConstants.AtticaDetail;
    public class AtticaDetail
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public AtticaRoofTypeEnum RoofType { get; set; }

        [Required]
        public AtticaWalkableEnum IsWalkable { get; set; }

        public AtticaScreedWaterproofingEnum? ScreedWaterproofing { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }

        public ICollection<AtticaDrain> AtticaDrains { get; set; } = new List<AtticaDrain>();
    }
}
