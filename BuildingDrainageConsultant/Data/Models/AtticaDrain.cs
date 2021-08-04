namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class AtticaDrain
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public double FlowRate { get; init; }

        [Required]
        public int DrainageArea { get; init; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; init; }

        [Required]
        public AtticaConcreteWaterproofingEnum ConcreteWaterproofing { get; init; }

        [Required]
        public AtticaDiameterEnum Diameter { get; init; }

        [Required]
        public AtticaVisiblePartEnum VisiblePart { get; init; }

        public int DrainageDetailId { get; set; }

        public AtticaDetail DrainageDetail { get; set; }

        public IEnumerable<AtticaPart> Parts { get; set; } = new List<AtticaPart>();
    }
}
