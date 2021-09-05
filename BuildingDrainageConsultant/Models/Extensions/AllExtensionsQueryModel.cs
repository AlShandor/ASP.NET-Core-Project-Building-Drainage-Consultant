namespace BuildingDrainageConsultant.Models.Extensions
{
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllExtensionsQueryModel
    {
        [Display(Name = "Search by name")]
        public string SearchTerm { get; set; }

        public IEnumerable<ExtensionServiceModel> Extensions { get; set; }
    }
}
