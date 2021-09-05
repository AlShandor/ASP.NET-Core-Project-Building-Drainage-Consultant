namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Merchants;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class MerchantsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Merchants.Any())
            {
                return;
            }

            var merchantService = serviceProvider.GetRequiredService<IMerchantService>();

            var merchants = new Merchant[]
            {
                new Merchant
                {
                    Name = "ViK Systems",
                    City = "Sofia",
                    Address = "Gorna Bania 45",
                    Email = "vik@abv.bg",
                    Latitude = 42.6923,
                    Longitude = 23.2790,
                    Phone = "+359 884 632 895",
                    Website = "www.viksystems.com"
                },
                new Merchant
                {
                    Name = "PipeSystems",
                    City = "Sofia",
                    Address = "Bul. Balgaria 145",
                    Email = "pipesystems@gmail.bg",
                    Latitude = 42.7139, 
                    Longitude = 23.3099,
                    Phone = "+359 884 276 895",
                    Website = "www.pipesystems.com"
                },
                new Merchant
                {
                    Name = "EuroStroy",
                    City = "Sofia",
                    Address = "ul. Sliwnica 235",
                    Email = "euro@gmail.bg",
                    Latitude = 42.6818, 
                    Longitude = 23.3330,
                    Phone = "+359 884 276 145",
                    Website = "www.eurostoy.com"
                },
            };

            merchantService.CreateAll(merchants);
        }

    }
}
