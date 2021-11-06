namespace BuildingDrainageConsultant.Models.SafeDrains
{
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using BuildingDrainageConsultant.Services.SafeDrains.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllSafeDrainsQueryModel
    {
        public int DrainsPerPage = 6;

        [Display(Name = "Search by drain name")]
        public string SearchTerm { get; set; }

        public SafeDrainDirectionEnum Direction { get; set; }
        public SafeDrainDiameterEnum Diameter { get; set; }
        public SafeDrainWaterproofingEnum Waterproofing { get; set; }
        public SafeDrainHeatingEnum Heating { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalDrains { get; set; }

        public SafeDrainSortingEnum Sorting { get; set; }

        public IEnumerable<SafeDrainListingServiceModel> Drains { get; set; }
    }
}
