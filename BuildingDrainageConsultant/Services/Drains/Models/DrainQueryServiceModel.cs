namespace BuildingDrainageConsultant.Services.Drains.Models
{
    using System.Collections.Generic;
    public class DrainQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int DrainsPerPage { get; set; }

        public int TotalDrains { get; set; }

        public IEnumerable<DrainDetailsServiceModel> Drains { get; set; }
    }
}