namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using Microsoft.Extensions.DependencyInjection;
    using System;
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
            var atticaPartsNames = atticaParts.Select(p => p.Name).ToArray();

            var atticaDrains = new AtticaDrain[]
            {
                new AtticaDrain
                {
                    FlowRate35mm = 3.6,
                    FlowRate100mm = 4.2,
                    DrainageArea35mm = 120,
                    DrainageArea100mm = 130,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.Bitumen,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                    Diameter = AtticaDiameterEnum.DN110,
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    AtticaDetailId = 1,
                    AtticaParts = atticaParts,
                    Name = string.Join(" + ", atticaPartsNames)
                },
                new AtticaDrain
                {
                    FlowRate35mm = 3.6,
                    FlowRate100mm = 4.2,
                    DrainageArea35mm = 120,
                    DrainageArea100mm = 130,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                    Diameter = AtticaDiameterEnum.DN75,
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    AtticaDetailId = 1,
                    AtticaParts = atticaParts,
                    Name = string.Join(" + ", atticaPartsNames)
                },
                new AtticaDrain
                {
                    FlowRate35mm = 3.6,
                    FlowRate100mm = 4.2,
                    DrainageArea35mm = 120,
                    DrainageArea100mm = 130,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.TPO,
                    Diameter = AtticaDiameterEnum.DN50,
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    AtticaDetailId = 2,
                    AtticaParts = atticaParts,
                    Name = string.Join(" + ", atticaPartsNames)
                },
                new AtticaDrain
                {
                    FlowRate35mm = 3.6,
                    FlowRate100mm = 4.2,
                    DrainageArea35mm = 120,
                    DrainageArea100mm = 130,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                    Diameter = AtticaDiameterEnum.DN110,
                    VisiblePart = AtticaVisiblePartEnum.FlatLeafCatcherWarmRoof,
                    AtticaDetailId = 2,
                    AtticaParts = atticaParts,
                    Name = string.Join(" + ", atticaPartsNames)
                },
            };

            atticaDrainService.CreateAll(atticaDrains);
        }

    }
}
