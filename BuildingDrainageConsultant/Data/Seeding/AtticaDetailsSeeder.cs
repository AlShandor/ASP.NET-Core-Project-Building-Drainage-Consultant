﻿namespace BuildingDrainageConsultant.Data.Seeding
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
                    VisiblePart = AtticaVisiblePartEnum.Grate,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL68H_0_110_B.jpg"
                },
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.ColdRoof,
                    IsWalkable = AtticaWalkableEnum.Walkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.PVC,
                    VisiblePart = AtticaVisiblePartEnum.FlatLeafCatcherWarmRoof,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL68F_0_75_B.jpg"
                },
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.WarmRoof,
                    IsWalkable = AtticaWalkableEnum.NotWalkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.TPO,
                    VisiblePart = AtticaVisiblePartEnum.FlatLeafCatcherWarmRoof,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL68H_0_110_B.jpg"
                },
                new AtticaDetail
                {
                    RoofType = AtticaRoofTypeEnum.ColdRoof,
                    IsWalkable = AtticaWalkableEnum.NotWalkable,
                    ScreedWaterproofing = AtticaScreedWaterproofingEnum.FlexibleMembrane2mm,
                    VisiblePart = AtticaVisiblePartEnum.FlatLeafCatcherWarmRoof,
                    Description = "Detail for Warm Roof. Walkable. With Bitumen Waterpoofing for the screed.",
                    ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL68P_0_50_B.jpg"
                },
            };

            articleService.CreateAll(articles);
        }
    }
}