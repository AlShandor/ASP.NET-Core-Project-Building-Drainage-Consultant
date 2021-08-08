namespace BuildingDrainageConsultant.Services.Merchants.Models
{
    public class MerchantServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string City { get; init; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
