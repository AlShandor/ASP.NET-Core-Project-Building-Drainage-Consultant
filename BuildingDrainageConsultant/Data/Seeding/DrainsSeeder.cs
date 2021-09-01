namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Services.Drains;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class DrainsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Drains.Any())
            {
                return;
            }

            var drainService = serviceProvider.GetRequiredService<IDrainService>();

            var drains = new Drain[]
            {
                new Drain
                {
                    Name = "HL64.2/H",
                    FlowRate = 3.4,
                    DrainageArea = 35,
                    Depth = 123,
                    Direction = DrainDirectionEnum.Horizontal,
                    Diameter = DrainDiameterEnum.DN110,
                    VisiblePart = DrainVisiblePartEnum.Grate,
                    Waterproofing = DrainWaterproofingEnum.Bitumen,
                    Heating = DrainHeatingEnum.NoHeating,
                    Renovation = DrainRenovationEnum.NoRenovation,
                    FlapSeal = DrainFlapSealEnum.NoFlapSeal,
                    ImageId = 1,
                    Description = "Воронка за използваем покрив/тераса, странично оттичане DN75, с топлоизолирано тяло, фланец за заваряване специално към мембрани от TPO/FPO на основа РР,"
                },
                new Drain
                {
                    Name = "HL62P",
                    FlowRate = 6.4,
                    DrainageArea = 100,
                    Depth = 64,
                    Direction = DrainDirectionEnum.Vertical,
                    Diameter = DrainDiameterEnum.DN110,
                    VisiblePart = DrainVisiblePartEnum.LeafCatcher,
                    Waterproofing = DrainWaterproofingEnum.Bitumen,
                    Heating = DrainHeatingEnum.NoHeating,
                    Renovation = DrainRenovationEnum.ForRenovation,
                    FlapSeal = DrainFlapSealEnum.NoFlapSeal,
                    ImageId = 1,
                    Description = "Воронка за неизпозваем покрив/тераса с долно оттичане DN125, с топлоизолирано тяло, вграден нагревател саморегулиращ се, за директно включване към мрежа 220V "
                },
                new Drain
                {
                    Name = "HL541",
                    FlowRate = 6.4,
                    DrainageArea = 78,
                    Depth = 48,
                    Direction = DrainDirectionEnum.Horizontal,
                    Diameter = DrainDiameterEnum.DN125,
                    VisiblePart = DrainVisiblePartEnum.LeafCatcher,
                    Waterproofing = DrainWaterproofingEnum.Bitumen,
                    Heating = DrainHeatingEnum.WithHeating,
                    Renovation = DrainRenovationEnum.ForRenovation,
                    FlapSeal = DrainFlapSealEnum.WithFlapSeal,
                    ImageId = 1,
                    Description = "Сифон за душ - PRIMUS-BLUE с хидроизолационна гарнитура и скоби за закрепване."
                },
                new Drain
                {
                    Name = "HL90-3020",
                    FlowRate = 5.8,
                    DrainageArea = 30,
                    Depth = 56,
                    Direction = DrainDirectionEnum.Horizontal,
                    Diameter = DrainDiameterEnum.DN75,
                    VisiblePart = DrainVisiblePartEnum.TilableFrame,
                    Waterproofing = DrainWaterproofingEnum.Bitumen,
                    Heating = DrainHeatingEnum.WithHeating,
                    Renovation = DrainRenovationEnum.ForRenovation,
                    FlapSeal = DrainFlapSealEnum.WithFlapSeal,
                    ImageId = 1,
                    Description = "Подов сифон за тераси и балкони DN40/50, странично оттичане, фланец за хидроизолацията(но без гарнитура), наставка с променяема височина 12-70 мм и рамка 123 х 123 мм,"
                },
                new Drain
                {
                    Name = "HL62.1F/2",
                    FlowRate = 6.9,
                    DrainageArea = 120,
                    Depth = 78,
                    Direction = DrainDirectionEnum.Vertical,
                    Diameter = DrainDiameterEnum.DN160,
                    VisiblePart = DrainVisiblePartEnum.TilableFrame,
                    Waterproofing = DrainWaterproofingEnum.PVC,
                    Heating = DrainHeatingEnum.WithHeating,
                    Renovation = DrainRenovationEnum.ForRenovation,
                    FlapSeal = DrainFlapSealEnum.WithFlapSeal,
                    ImageId = 1,
                    Description = "Воронка за неизпозваем покрив/тераса с долно оттичане DN125, с топлоизолирано тяло, вграден нагревател саморегулиращ се, за директно включване към мрежа 220V (10-30 Wat), фланец за"
                },
                new Drain
                {
                    Name = "HL62.1/1",
                    FlowRate = 6.4,
                    DrainageArea = 89,
                    Depth = 38,
                    Direction = DrainDirectionEnum.Vertical,
                    Diameter = DrainDiameterEnum.DN125,
                    VisiblePart = DrainVisiblePartEnum.TilableFrame,
                    Waterproofing = DrainWaterproofingEnum.TPO,
                    Heating = DrainHeatingEnum.WithHeating,
                    Renovation = DrainRenovationEnum.NoRenovation,
                    FlapSeal = DrainFlapSealEnum.WithFlapSeal,
                    ImageId = 1,
                    Description = "Воронка за неизпозваем покрив/тераса с долно оттичане DN125, с топлоизолирано тяло, вграден нагревател саморегулиращ се, за директно включване към мрежа 220V (10-30 Wat), фланец за"
                },
                new Drain
                {
                    Name = "HL62.1BP/1",
                    FlowRate = 6.4,
                    DrainageArea = 89,
                    Depth = 38,
                    Direction = DrainDirectionEnum.Horizontal,
                    Diameter = DrainDiameterEnum.DN75,
                    VisiblePart = DrainVisiblePartEnum.LeafCatcher,
                    Waterproofing = DrainWaterproofingEnum.TPO,
                    Heating = DrainHeatingEnum.WithHeating,
                    Renovation = DrainRenovationEnum.NoRenovation,
                    FlapSeal = DrainFlapSealEnum.WithFlapSeal,
                    ImageId = 1,
                    Description = "Воронка за неизпозваем покрив/тераса с долно оттичане DN125, с топлоизолирано тяло, вграден нагревател саморегулиращ се, за директно включване към мрежа 220V (10-30 Wat), фланец за"
                },
                new Drain
                {
                    Name = "HL615",
                    FlowRate = 6.4,
                    DrainageArea = 89,
                    Depth = 76,
                    Direction = DrainDirectionEnum.Horizontal,
                    Diameter = DrainDiameterEnum.DN110,
                    VisiblePart = DrainVisiblePartEnum.LeafCatcher,
                    Waterproofing = DrainWaterproofingEnum.Bitumen,
                    Heating = DrainHeatingEnum.NoHeating,
                    Renovation = DrainRenovationEnum.NoRenovation,
                    FlapSeal = DrainFlapSealEnum.WithFlapSeal,
                    ImageId = 1,
                    Description = "Воронка за неизпозваем покрив/тераса с долно оттичане DN125, с топлоизолирано тяло, вграден нагревател саморегулиращ се, за директно включване към мрежа 220V (10-30 Wat), фланец за"
                },
            };

            drainService.CreateAll(drains);
        }

    }
}
