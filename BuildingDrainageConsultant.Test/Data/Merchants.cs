namespace BuildingDrainageConsultant.Test.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Merchants
    {
        public static IEnumerable<Merchant> TenMerchants
            => Enumerable.Range(0, 10).Select(i => new Merchant());

        public static IEnumerable<Merchant> ValidMerchant(int partId)
            => Enumerable.Range(1, 1).Select(i => new Merchant
            {
                Name = "ValidAtticaDrain",
                City = "Sofia",
                Address = "Address String",
                Email = "asd@abv.bg",
                Latitude = 42.2345,
                Longitude = 23.5673,
                Phone = "+3598888888",
                Website = "www.example.com"
            });
    }
}
