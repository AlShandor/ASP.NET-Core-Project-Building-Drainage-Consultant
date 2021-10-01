namespace BuildingDrainageConsultant.Services.Drains
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System.Collections.Generic;

    public interface IDrainService
    {
        public DrainQueryServiceModel All(
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
            int? imageId,
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
            int? imageId,
            string description);

        public DrainServiceModel Details(int id);

        public bool Delete(int id);

        public IEnumerable<DrainDetailsServiceModel> ByUser(string userId);

        public ICollection<WaterproofingKitServiceModel> GetWaterproofingKits();

        public ICollection<AccessoryServiceModel> GetAccessories();

        public ICollection<ExtensionServiceModel> GetExtensions();

        public bool AddWaterproofingKit(int waterproofingKitId, int drainId);

        public bool AddAccessory(int accessoryId, int drainId);

        public bool AddExtension(int extensionId, int drainId);

        public bool RemoveWaterproofingKit(int kitId, int drainId);

        public bool RemoveAccessory(int accessoryId, int drainId);

        public bool RemoveExtension(int extensionId, int drainId);

        public bool AddToMine(string userId, int drainId);

        public bool RemoveFromMine(string userId, int drainId);

        public bool IsMyDrain(int drainId, string userId);

        public int GetImageIdByName(string name);

        public int? GetWaterproofingKitId(string kitName);

        public ICollection<Accessory> GetAccessoriesFromString(string accessoriesString);
    }
}