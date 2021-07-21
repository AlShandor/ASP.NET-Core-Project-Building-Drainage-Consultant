namespace BuildingDrainageConsultant.Services.Drains
{
    public interface IDrainService
    {
        DrainQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int drainsPerPage);
    }
}
