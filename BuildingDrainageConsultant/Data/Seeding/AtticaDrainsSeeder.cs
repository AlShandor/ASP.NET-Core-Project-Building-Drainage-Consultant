namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AtticaDrainsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AtticaDrains.Any())
            {
                return;
            }

            var atticaDrainService = serviceProvider.GetRequiredService<IAtticaDrainService>();

            var atticaParts = dbContext.AtticaParts.Take(3).ToList();

            var atticaDrains = new AtticaDrain[]
            {
                new AtticaDrain
                {
                    Name = "HL68P.0/110",
                    FlowRate = 3.6,
                    DrainageArea = 120,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.Bitumen,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                    Diameter = AtticaDiameterEnum.DN110,
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    AtticaDetailId = 1,
                    AtticaParts = atticaParts
                },
                new AtticaDrain
                {
                    Name = "HL68P.HR",
                    FlowRate = 3.6,
                    DrainageArea = 120,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                    Diameter = AtticaDiameterEnum.DN75,
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    AtticaDetailId = 1,
                    AtticaParts = atticaParts
                },
                new AtticaDrain
                {
                    Name = "HL690TH",
                    FlowRate = 3.6,
                    DrainageArea = 120,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.TPO,
                    Diameter = AtticaDiameterEnum.DN50,
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    AtticaDetailId = 2,
                    AtticaParts = atticaParts
                },
                new AtticaDrain
                {
                    Name = "HL609HB",
                    FlowRate = 3.6,
                    DrainageArea = 120,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                    Diameter = AtticaDiameterEnum.DN110,
                    VisiblePart = AtticaVisiblePartEnum.FlatLeafCatcherWarmRoof,
                    AtticaDetailId = 2,
                    AtticaParts = atticaParts
                },
            };

            atticaDrainService.CreateAll(atticaDrains);
        }

    }
}
