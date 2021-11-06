namespace BuildingDrainageConsultant.Services.AtticaDrains.Models
{
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;

    public class AtticaDrainListingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AtticaPartServiceModel> AtticaParts { get; set; } = new List<AtticaPartServiceModel>();
    }
}
