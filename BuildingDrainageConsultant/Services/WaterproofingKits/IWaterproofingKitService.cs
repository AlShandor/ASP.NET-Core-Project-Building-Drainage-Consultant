namespace BuildingDrainageConsultant.Services.WaterproofingKits
{
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System.Collections.Generic;

    public interface IWaterproofingKitService
    {
        public IEnumerable<WaterproofingKitServiceModel> All(string searchTerm);

        public WaterproofingKitServiceModel Details(int id);
        public int Create(
                string name,
                int? imageId,
                string description);

        public bool Edit(
            int id,
            string name,
            int? imageId,
            string description);

        public bool Delete(int id);

        public int GetImageIdByName(string name);
    }
}
