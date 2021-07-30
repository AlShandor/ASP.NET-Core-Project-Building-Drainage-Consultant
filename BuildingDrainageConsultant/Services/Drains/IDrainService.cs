namespace BuildingDrainageConsultant.Services.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums;
    using BuildingDrainageConsultant.Services.Drains.Models;

    public interface IDrainService
    {
        DrainQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int drainsPerPage);

        public DrainServiceModel Details(int id);

        public int Create(
            string name,
            double flowRate,
            int drainageArea,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            bool hasHeating,
            bool forRenovation,
            bool hasFlapSeal,
            string imageUrl,
            string description);
    }
}
