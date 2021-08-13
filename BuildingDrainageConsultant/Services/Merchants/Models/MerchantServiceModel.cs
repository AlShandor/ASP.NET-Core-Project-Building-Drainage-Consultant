namespace BuildingDrainageConsultant.Services.Merchants.Models
{
    public class MerchantServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}