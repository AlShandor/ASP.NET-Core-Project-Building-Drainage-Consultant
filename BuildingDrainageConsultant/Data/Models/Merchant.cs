namespace BuildingDrainageConsultant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Seller;
    public class Merchant
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; init; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [MaxLength(WebsiteMaxLength)]
        public string WebSite { get; set; }
    }
}
