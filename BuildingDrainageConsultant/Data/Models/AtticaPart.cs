namespace BuildingDrainageConsultant.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class AtticaPart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; init; }

        [Url]
        public string ImageUrl { get; init; }

        [Required]
        public string Description { get; init; }
    }
}
