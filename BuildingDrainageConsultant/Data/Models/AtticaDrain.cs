namespace BuildingDrainageConsultant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class AtticaDrain
    {
        [Key]
        public int Id { get; init; }

        public string Name { get; init; }

        [Required]
        public double FlowRate { get; init; }

        [Required]
        public int DrainageArea { get; init; }

        public string ScreedWaterproofing { get; init; }

        [Required]
        public string ConcreteWaterproofing { get; init; }

        [Required]

        public int Diameter { get; init; }

        [Required]

        public string VisiblePart { get; init; }

        public int DrainageDetailId { get; set; }


        public DrainageDetail DrainageDetail { get; set; }

        public IEnumerable<AtticaPart> Parts { get; set; } = new List<AtticaPart>();
    }
}
