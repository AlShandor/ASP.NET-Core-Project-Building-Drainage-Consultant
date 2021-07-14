namespace BuildingDrainageConsultant.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Seller
    {
        [Key]
        public int Id { get; init; }

        [Required]

        public string Name { get; init; }

        [Required]

        public string City { get; init; }

        [Required]

        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string WebSite { get; set; }
    }
}
