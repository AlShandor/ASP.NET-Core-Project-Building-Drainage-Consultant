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
                    ImageId = 1,
                    Description = "Листоуловител за барбакан от серията HL68"
                },
                new AtticaPart
                {
                    Name = "HL68P.0/110",
                    ImageId = 1,
                    Description = "Барбакан с PVC-фланец за хидроизолация и PP-отводна тръба DN110, подходящ за монтаж непосредствено до борд на покрив/тераса и заваряване с PVC -мембранни хидроизолации"
                },
                new AtticaPart
                {
                    Name = "HL068.10E",
                    ImageId = 1,
                    Description = "PP-отводна тръба DN110 с уплътнителни пръстени за барбакани от серията HL68"
                },
            };

            atticaPartService.CreateAll(atticaParts);
        }
    }

}
