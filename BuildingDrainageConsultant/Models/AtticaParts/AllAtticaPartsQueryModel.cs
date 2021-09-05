namespace BuildingDrainageConsultant.Models.AtticaParts
{
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllAtticaPartsQueryModel
    {
        [Display(Name = "Search by name")]
        public string SearchTerm { get; set; }

        public IEnumerable<AtticaPartServiceModel> AtticaParts { get; set; }
    }
}
