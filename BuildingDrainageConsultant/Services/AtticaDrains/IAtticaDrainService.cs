namespace BuildingDrainageConsultant.Services.AtticaDrains
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;

    public interface IAtticaDrainService
    {
        public IEnumerable<AtticaDrainServiceModel> All(string searchTerm);

        public AtticaDrainQueryServiceModel SearchAtticaDrains(
            int atticaDetailId,
            string searchTerm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaDrainSortingEnum sorting,
            int currentPage,
            int drainsPerPage);

        public int Create(
            int detailId,
            string name,
            double flowRate35mm,
            double flowRate100mm,
            int drainageArea35mm,
            int drainageArea100mm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart);

        public bool Edit(
            int id,
            double flowRate35mm,
            double flowRate100mm,
            int drainageArea35mm,
            int drainageArea100mm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart);

        public AtticaDrainServiceModel Details(int id);

        public bool Delete(int id);

        public IEnumerable<AtticaDetailServiceModel> GetAtticaDetails();

        public AtticaDetailServiceModel GetAtticaDetailById(int id);

        public IEnumerable<AtticaPartServiceModel> GetAtticaPartsForDrain(int id);

        public IEnumerable<AtticaPartServiceModel> GetAtticaParts();

        public AtticaPartServiceModel GetAtticaPartById(int id);

        public bool AddAtticaPart(int partId, int drainId);

        public IEnumerable<AtticaDrainServiceModel> ByUser(string userId);

        public bool AddToMine(string userId, int atticaDrainId);

        public bool IsMyAtticaDrain(int atticaDrainId, string userId);

        public void CreateAll(AtticaDrain[] atticaDrain);

        public bool RemovePart(int partId, int atticaDrainId);

        public bool RemoveFromMine(string userId, int drainId);
    }
}