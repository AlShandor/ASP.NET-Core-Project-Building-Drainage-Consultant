namespace BuildingDrainageConsultant.Services.AtticaDrains.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;

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

    }
}