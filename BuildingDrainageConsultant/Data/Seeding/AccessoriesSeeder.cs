namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Accessories;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class AccessoriesSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Accessories.Any())
            {
                return;
            }

            var accessoryService = serviceProvider.GetRequiredService<IAccessoryService>();

            var accessories = new Accessory[]
            {
                new Accessory
                {
                    Name = "HL151",
                    ImageId = 1,
                    Description = "Сито/уловител за речен филц, за сифоните за тераси, от серията HL3100T и HL5100T"
                },
                new Accessory
                {
                    Name = "HL080.8E",
                    ImageId = 1,
                    Description = "Листоуловител d 110мм за HL80, HL90, HL310N.2"
                },
                new Accessory
                {
                    Name = "HL180",
                    ImageId = 1,
                    Description = "Отводнителен пръстен за серия HL80, d 110мм"
                },
            };

            accessoryService.CreateAll(accessories);
        }
    }
}
