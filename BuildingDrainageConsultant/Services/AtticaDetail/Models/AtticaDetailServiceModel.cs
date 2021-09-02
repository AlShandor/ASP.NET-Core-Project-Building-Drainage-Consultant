using BuildingDrainageConsultant.Data.Models;
using BuildingDrainageConsultant.Data.Models.Enums.Attica;

namespace BuildingDrainageConsultant.Services.AtticaDetail.Models
{
    public class AtticaDetailServiceModel
    {
        public int Id { get; set; }

        public AtticaRoofTypeEnum RoofType { get; set; }

        public AtticaWalkableEnum IsWalkable { get; set; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}