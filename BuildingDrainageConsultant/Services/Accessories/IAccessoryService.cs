namespace BuildingDrainageConsultant.Services.Accessories
{
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using System.Collections.Generic;

    public interface IAccessoryService
    {
        public IEnumerable<AccessoryServiceModel> All(string searchTerm);

        public AccessoryServiceModel Details(int id);
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
