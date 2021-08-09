namespace BuildingDrainageConsultant.Models.AtticaDrains
{
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllAtticaPartsQueryModel
    {
        [Display(Name = "Search by drain name")]
        public string SearchTerm { get; init; }

        public IEnumerable<AtticaPartServiceModel> AtticaParts { get; set; }
    }
}
