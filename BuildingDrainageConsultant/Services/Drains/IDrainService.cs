namespace BuildingDrainageConsultant.Services.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using System.Collections.Generic;

    public interface IDrainService
    {
        DrainQueryServiceModel All(
            string searchTerm,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            DrainSortingEnum sorting,
            int currentPage,
            int drainsPerPage);

        public IEnumerable<DrainDetailsServiceModel> ByUser(string userId);

        public DrainServiceModel Details(int id);

        public int Create(
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            string imageUrl,
            string description);

        public bool Edit(
            int id,
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            string imageUrl,
            string description);

        public bool Delete(int id);

        public bool AddToMine(string userId, int drainId);
    }
}
