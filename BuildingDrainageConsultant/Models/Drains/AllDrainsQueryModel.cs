namespace BuildingDrainageConsultant.Models.Drains
{
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllDrainsQueryModel
    {
        public const int DrainsPerPage = 6;

        [Display(Name = "Search by drain name")]
        public string SearchTerm { get; set; }

        public DrainDirectionEnum Direction { get; set; }
        public DrainDiameterEnum Diameter { get; set; }
        public DrainVisiblePartEnum VisiblePart { get; set; }
        public DrainWaterproofingEnum Waterproofing { get; set; }
        public DrainHeatingEnum Heating { get; set; }
        public DrainRenovationEnum Renovation { get; set; }
        public DrainFlapSealEnum FlapSeal { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalDrains { get; set; }

        public DrainSortingEnum Sorting { get; set; }

        public IEnumerable<DrainDetailsServiceModel> Drains { get; set; }
    }
}