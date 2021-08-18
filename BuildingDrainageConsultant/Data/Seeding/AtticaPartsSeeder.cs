namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class AtticaPartsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AtticaParts.Any())
            {
                return;
            }

            var atticaPartService = serviceProvider.GetRequiredService<IAtticaPartService>();

            var atticaParts = new AtticaPart[]
            {
                new AtticaPart
                {
                    Name = "HL068.1E",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL068_1E_B.jpg",
                    Description = "Листоуловител за барбакан от серията HL68"
                },
                new AtticaPart
                {
                    Name = "HL68P.0/110",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL68P_0_110_B.jpg",
                    Description = "Барбакан с PVC-фланец за хидроизолация и PP-отводна тръба DN110, подходящ за монтаж непосредствено до борд на покрив/тераса и заваряване с PVC -мембранни хидроизолации"
                },
                new AtticaPart
                {
                    Name = "HL068.10E",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL068_10E_B.jpg",
                    Description = "PP-отводна тръба DN110 с уплътнителни пръстени за барбакани от серията HL68"
                },
            };

            atticaPartService.CreateAll(atticaParts);
        }
    }

}
