﻿namespace BuildingDrainageConsultant.Models.AtticaDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AtticaDrainFormModel
    {
        public int Id { get; set; }

        [Required]
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

        [Display(Name = "AtticaDetail")]
        public int AtticaDetailId { get; set; }

        [Display(Name = "Attica Parts")]
        public IEnumerable<AtticaPartServiceModel> AtticaParts { get; set; } = new List<AtticaPartServiceModel>();
    }
}