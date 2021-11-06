namespace BuildingDrainageConsultant.Services.SafeDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using BuildingDrainageConsultant.Services.SafeDrains.Models;
    using System.Collections.Generic;

    public interface ISafeDrainService
    {
        public SafeDrainQueryServiceModel All(
             string searchTerm,
             SafeDrainDirectionEnum direction,
             SafeDrainDiameterEnum diameter,
             SafeDrainWaterproofingEnum waterproofing,
             SafeDrainHeatingEnum heating,
             SafeDrainSortingEnum sorting,
             int currentPage,
             int drainsPerPage);

        public int Create(
            string name,
            double flowRateFree,
            double flowRate3mVertical,
            int drainageAreaFree,
            int draingeArea3mVertical,
            int depth,
            SafeDrainDirectionEnum direction,
            SafeDrainDiameterEnum diameter,
            SafeDrainWaterproofingEnum waterproofing,
            SafeDrainHeatingEnum heating,
            int? imageId,
            string description);

        public bool Edit(
            int id,
            string name,
            double flowRateFree,
            double flowRate3mVertical,
            int drainageAreaFree,
            int draingeArea3mVertical,
            int depth,
            SafeDrainDirectionEnum direction,
            SafeDrainDiameterEnum diameter,
            SafeDrainWaterproofingEnum waterproofing,
            SafeDrainHeatingEnum heating,
            int? imageId,
            string description);

        public SafeDrainServiceModel Details(int id);

        public bool Delete(int id);

        public IEnumerable<SafeDrainListingServiceModel> ByUser(string userId);

        public bool AddToMine(string userId, int drainId);

        public bool RemoveFromMine(string userId, int drainId);

        public bool IsMyDrain(int drainId, string userId);

        public int GetImageIdByName(string name);
    }
}
