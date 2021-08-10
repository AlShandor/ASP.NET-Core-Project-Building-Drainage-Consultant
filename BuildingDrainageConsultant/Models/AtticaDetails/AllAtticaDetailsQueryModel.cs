namespace BuildingDrainageConsultant.Models.AtticaDetails
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using System.Collections.Generic;
    public class AllAtticaDetailsQueryModel
    {
        public AtticaRoofTypeEnum RoofType { get; set; }

        public AtticaWalkableEnum IsWalkable { get; set; }

        public IEnumerable<AtticaDetailServiceModel> AtticaDetails { get; set; }

        public int TotalDrains { get; set; }
    }
}
