namespace BuildingDrainageConsultant.Models.AtticaDetails
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using System.Collections.Generic;
    public class AllAtticaDetailsQueryModel
    {
        public AtticaRoofTypeEnum RoofType { get; init; }

        public AtticaWalkableEnum IsWalkable { get; init; }

        public IEnumerable<AtticaDetailServiceModel> AtticaDetails { get; set; }

        public int TotalDrains { get; set; }
    }
}
