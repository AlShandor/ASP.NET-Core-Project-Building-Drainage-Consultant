namespace BuildingDrainageConsultant.Models.Accessories
{
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllAccessoriesQueryModel
    {
        [Display(Name = "Search by name")]
        public string SearchTerm { get; set; }

        public IEnumerable<AccessoryServiceModel> Accessories { get; set; }
    }
}
