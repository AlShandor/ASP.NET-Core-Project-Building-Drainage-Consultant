namespace BuildingDrainageConsultant.Models.Merchants
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Merchant;
    public class MerchantFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [MaxLength(WebsiteMaxLength)]
        public string Website { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
