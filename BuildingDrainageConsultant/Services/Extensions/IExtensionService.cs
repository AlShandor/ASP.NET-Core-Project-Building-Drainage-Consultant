namespace BuildingDrainageConsultant.Services.Extensions
{
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using System.Collections.Generic;

    public interface IExtensionService
    {
        public IEnumerable<ExtensionServiceModel> All(string searchTerm);

        public int Create(
                string name,
                int? imageId,
                string description);

        public bool Edit(
            int id,
            string name,
            int? imageId,
            string description);

        public ExtensionServiceModel Details(int id);

        public bool Delete(int id);

        public int GetImageIdByName(string name);
    }
}
