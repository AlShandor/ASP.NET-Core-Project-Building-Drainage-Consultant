namespace BuildingDrainageConsultant.Services.AtticaParts
{
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    public interface IAtticaPartService
    {
        public IEnumerable<AtticaPartServiceModel> All(string searchTerm);

        public AtticaPartServiceModel Details(int id);
        public int Create(
                string name,
                string imageUrl,
                string description);

        public bool Edit(
            int id,
            string name,
            string imageUrl,
            string description);

        public bool Delete(int id);
    }
}
