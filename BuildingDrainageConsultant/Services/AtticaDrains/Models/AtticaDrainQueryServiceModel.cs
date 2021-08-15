namespace BuildingDrainageConsultant.Services.AtticaDrains.Models
{
    using System.Collections.Generic;

    public class AtticaDrainQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int DrainsPerPage { get; set; }

        public int TotalDrains { get; set; }

        public IEnumerable<AtticaDrainServiceModel> AtticaDrains { get; set; }
    }
}
