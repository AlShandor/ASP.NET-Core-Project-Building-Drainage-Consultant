namespace BuildingDrainageConsultant.Models.Merchants
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Merchant;
    public class MerchantFormModel
    {
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
        public string Website { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
