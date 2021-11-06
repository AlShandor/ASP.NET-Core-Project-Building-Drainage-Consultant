namespace BuildingDrainageConsultant.Models.AtticaDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using System.Collections.Generic;
    public class AllAtticaDrainsQueryModel
    {
        public int DrainsPerPage = 6;

        public string SearchTerm { get; set; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        public AtticaConcreteWaterproofingEnum ConcreteWaterproofing { get; set; }

        public AtticaDiameterEnum Diameter { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalDrains { get; set; }

        public AtticaDrainSortingEnum Sorting { get; set; }

        public IEnumerable<AtticaDrainListingModel> Drains { get; set; }

        public int AtticaDetailId { get; set; }

        public AtticaDetailServiceModel AtticaDetail { get; set; }
    }
}