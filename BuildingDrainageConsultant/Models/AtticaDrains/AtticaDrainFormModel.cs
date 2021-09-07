namespace BuildingDrainageConsultant.Models.AtticaDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AtticaDrainFormModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "Flow Rate (l/s)")]
        public double FlowRate { get; set; }

        [Required]
        [Display(Name = "Drainage Area (m²)")]
        public int DrainageArea { get; set; }

        [Display(Name = "Screed Waterproofing")]
        public AtticaScreedWaterproofingEnum ScreedWaterproofing { get; set; }

        [Required]
        [Display(Name = "Concrete Waterproofing")]
        public AtticaConcreteWaterproofingEnum ConcreteWaterproofing { get; set; }

        [Required]
        public AtticaDiameterEnum Diameter { get; set; }

        [Required]
        [Display(Name = "Visible Part")]
        public AtticaVisiblePartEnum VisiblePart { get; set; }

        public int AtticaPartId { get; set; }

        public int AtticaDetailId { get; set; }

        public AtticaDetailServiceModel AtticaDetail { get; set; }

        public bool IsMyAtticaDrain { get; set; }

        public IEnumerable<AtticaDetailServiceModel> AtticaDetails { get; set; }

        public IEnumerable<AtticaPartServiceModel> AtticaParts { get; set; }

    }
}
