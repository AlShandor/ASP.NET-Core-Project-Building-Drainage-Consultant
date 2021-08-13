namespace BuildingDrainageConsultant.Services.AtticaDetail
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;

    public interface IAtticaDetailService
    {
        public AtticaDetailQueryServiceModel All(AtticaRoofTypeEnum roofType, AtticaWalkableEnum isWalkable);

        public AtticaDetailServiceModel Details(int id);

        public int Create(
                AtticaRoofTypeEnum roofType,
                AtticaWalkableEnum isWalkable,
                AtticaScreedWaterproofingEnum screedWaterproofing,
                AtticaVisiblePartEnum visiblePart,
                string description,
                string imageUrl);

        public bool Edit(
            int id,
            AtticaRoofTypeEnum roofType,
            AtticaWalkableEnum isWalkable,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaVisiblePartEnum visiblePart,
            string description,
            string imageUrl);

        public bool Delete(int id);
    }
}