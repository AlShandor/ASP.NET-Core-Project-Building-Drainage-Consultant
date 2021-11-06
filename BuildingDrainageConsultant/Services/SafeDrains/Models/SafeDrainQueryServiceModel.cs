namespace BuildingDrainageConsultant.Services.SafeDrains.Models
{
    using System.Collections.Generic;
    public class SafeDrainQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int DrainsPerPage { get; set; }

        public int TotalDrains { get; set; }

        public IEnumerable<SafeDrainListingServiceModel> Drains { get; set; }
    }
}
