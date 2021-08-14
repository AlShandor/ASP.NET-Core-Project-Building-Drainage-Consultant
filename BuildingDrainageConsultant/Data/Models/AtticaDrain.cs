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
        public string Name { get; set; }

        [Required]
        public double FlowRate { get; set; }

        [Required]
        public int DrainageArea { get; set; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        [Required]
        public AtticaConcreteWaterproofingEnum ConcreteWaterproofing { get; set; }

        [Required]
        public AtticaDiameterEnum Diameter { get; set; }

        [Required]
        public AtticaVisiblePartEnum VisiblePart { get; set; }

        public int? AtticaDetailId { get; set; }

        public AtticaDetail AtticaDetail { get; set; }

        public ICollection<AtticaPart> AtticaParts { get; set; } = new List<AtticaPart>();

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
