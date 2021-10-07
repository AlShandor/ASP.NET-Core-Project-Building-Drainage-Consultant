namespace BuildingDrainageConsultant.Test.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using System.Collections.Generic;
    using System.Linq;

    public static class AtticaDrains
    {
        public static IEnumerable<AtticaDrain> TenAtticaDrains
            => Enumerable.Range(0, 10).Select(i => new AtticaDrain());

        public static IEnumerable<AtticaDrain> ValidAtticaDrain(int drainId)
            => Enumerable.Range(1, 1).Select(i => new AtticaDrain
            {
                Id = drainId,
                Name = "ValidAtticaDrain",
                FlowRate35mm = 1.1,
                FlowRate100mm = 2.2,
                DrainageArea35mm = 3,
                DrainageArea100mm = 4,
                ScreedWaterproofing = AtticaScreedWaterproofingEnum.Bitumen,
                ConcreteWaterproofing = AtticaConcreteWaterproofingEnum.Bitumen,
                Diameter = AtticaDiameterEnum.DN110,
                VisiblePart = AtticaVisiblePartEnum.DomedLeafCatcherWarmRoof
            });
    }
}
