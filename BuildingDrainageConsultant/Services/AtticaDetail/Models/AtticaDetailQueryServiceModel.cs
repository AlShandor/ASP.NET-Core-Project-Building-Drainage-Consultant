namespace BuildingDrainageConsultant.Services.AtticaDetail.Models
{
    using System.Collections.Generic;

    public class AtticaDetailQueryServiceModel
    {
        public int TotalDrains { get; set; }

        public IEnumerable<AtticaDetailServiceModel> AtticaDetails { get; init; }
    }
}
