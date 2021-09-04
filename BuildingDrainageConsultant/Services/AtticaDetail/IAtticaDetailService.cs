namespace BuildingDrainageConsultant.Services.AtticaDetail
{
    using BuildingDrainageConsultant.Data.Models;
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
                string description,
                int? imageId);

        public bool Edit(
            int id,
            AtticaRoofTypeEnum roofType,
            AtticaWalkableEnum isWalkable,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            string description,
            int? imageId);

        public bool Delete(int id);

        public void CreateAll(AtticaDetail[] atticaDetails);
    }
}