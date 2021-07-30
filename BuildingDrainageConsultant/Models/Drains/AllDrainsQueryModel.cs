namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Services.Drains.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllDrainsQueryModel
    {
        public const int DrainsPerPage = 6;

        [Display(Name = "Search by drain name")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalDrains { get; set; }

        public IEnumerable<DrainDetailsServiceModel> Drains { get; set; }
    }
}
