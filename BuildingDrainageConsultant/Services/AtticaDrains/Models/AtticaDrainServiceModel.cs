namespace BuildingDrainageConsultant.Services.AtticaDrains.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;

    public class AtticaDrainServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double FlowRate { get; set; }

        public int DrainageArea { get; set; }

        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        public AtticaConcreteWaterproofingEnum ConcreteWaterproofing { get; set; }

        public AtticaDiameterEnum Diameter { get; set; }

        public AtticaVisiblePartEnum VisiblePart { get; set; }

        public int AtticaDetailId { get; set; }

        public AtticaDetailServiceModel AtticaDetail { get; set; }

        public IEnumerable<AtticaPartServiceModel> AtticaParts { get; set; }
    }
}