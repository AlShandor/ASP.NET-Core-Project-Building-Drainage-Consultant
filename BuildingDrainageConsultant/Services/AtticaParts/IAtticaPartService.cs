namespace BuildingDrainageConsultant.Services.AtticaParts
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    public interface IAtticaPartService
    {
        public IEnumerable<AtticaPartServiceModel> All(string searchTerm);

        public int Create(
                string name,
                int? imageId,
                string description);

        public bool Edit(
            int id,
            string name,
            int? imageId,
            string description);

        public AtticaPartServiceModel Details(int id);

        public bool Delete(int id);

        public int GetImageIdByName(string name);

        public AtticaPart GetAtticaPartByName(string name);
    }
}