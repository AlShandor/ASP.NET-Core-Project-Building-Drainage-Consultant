namespace BuildingDrainageConsultant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class DrainageDetail
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        public string ScreedWaterproofing { get; init; }

        public string VisiblePart { get; init; }

        public IEnumerable<AtticaDrain> AtticaDrains { get; set; } = new List<AtticaDrain>();
    }
}
