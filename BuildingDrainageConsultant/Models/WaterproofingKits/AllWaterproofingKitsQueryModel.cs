namespace BuildingDrainageConsultant.Models.WaterproofingKits
{
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllWaterproofingKitsQueryModel
    {
        [Display(Name = "Search by drain name")]
        public string SearchTerm { get; set; }

        public IEnumerable<WaterproofingKitServiceModel> WaterproofingKits { get; set; }
    }
}
