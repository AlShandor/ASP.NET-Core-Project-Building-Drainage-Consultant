namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class AtticaDetailsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AtticaDetails.Any())
            {
                return;
            }

            var articleService = serviceProvider.GetRequiredService<IAtticaDetailService>();

            var articles = new AtticaDetail[]
            {
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.WarmRoof,
                    IsWalkable = AtticaWalkableEnum.Walkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.Bitumen,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageId = 1,
                },
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.ColdRoof,
                    IsWalkable = AtticaWalkableEnum.Walkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageId = 1,
                },
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.WarmRoof,
                    IsWalkable = AtticaWalkableEnum.NotWalkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.TPO,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageId = 1,
                },
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.ColdRoof,
                    IsWalkable = AtticaWalkableEnum.NotWalkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.FlexibleMembrane2mm,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageId = 1,
                },
            };

            articleService.CreateAll(articles);
        }
    }
}
