namespace BuildingDrainageConsultant.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class DrainageDetail
    {
        [Key]
        public int Id { get; init; }

        [Required]

        public string ImageUrl { get; init; }

        public string ScreedWaterproofing { get; init; }

        public string VisiblePart { get; init; }
    }
}
