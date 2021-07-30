namespace BuildingDrainageConsultant.Services.Drains
{
    using BuildingDrainageConsultant.Services.Drains.Models;

    public interface IDrainService
    {
        DrainQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int drainsPerPage);

        public DrainServiceModel Details(int id);
    }
}
