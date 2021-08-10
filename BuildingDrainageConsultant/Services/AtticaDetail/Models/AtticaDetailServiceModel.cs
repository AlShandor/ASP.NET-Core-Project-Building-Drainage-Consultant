using BuildingDrainageConsultant.Data.Models.Enums.Attica;

namespace BuildingDrainageConsultant.Services.AtticaDetail.Models
{
    public class AtticaDetailServiceModel
    {
        public int Id { get; set; }

        public AtticaRoofTypeEnum RoofType { get; set; }

        public AtticaWalkableEnum IsWalkable { get; set; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        public AtticaVisiblePartEnum VisiblePart { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
