namespace BuildingDrainageConsultant.Test.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using System.Collections.Generic;
    using System.Linq;

    public class AtticaDetails
    {
        public static IEnumerable<AtticaDetail> TenAtticaDetails
            => Enumerable.Range(0, 10).Select(i => new AtticaDetail());

        public static IEnumerable<AtticaDetail> ValidAtticaDetail(int atticaDetailId)
            => Enumerable.Range(1, 1).Select(i => new AtticaDetail
            {
                Id = atticaDetailId,
                RoofType = AtticaRoofTypeEnum.ColdRoof,
                IsWalkable = AtticaWalkableEnum.NotWalkable,
                ScreedWaterproofing = AtticaScreedWaterproofingEnum.Bitumen,
                Description = "AtticaDrain Description",
                ImageUrl = "https://hl-bg.bg/images/stories/virtuemart/product/HL62.1F_2_502e4143486ae.jpg"
            });
    }
}
