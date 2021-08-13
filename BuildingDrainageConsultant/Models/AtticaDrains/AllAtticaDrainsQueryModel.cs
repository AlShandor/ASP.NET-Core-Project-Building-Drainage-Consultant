namespace BuildingDrainageConsultant.Models.AtticaDrains
{
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using System.Collections.Generic;
    public class AllAtticaDrainsQueryModel
    {
        public string SearchTerm { get; set; }

        public IEnumerable<AtticaDrainServiceModel> AtticaDrains { get; set; }
    }
}