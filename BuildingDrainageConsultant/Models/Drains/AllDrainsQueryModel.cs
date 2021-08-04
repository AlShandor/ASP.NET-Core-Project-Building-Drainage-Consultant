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
        public string SearchTerm { get; init; }

        public DrainDirectionEnum Direction { get; init; }
        public DrainDiameterEnum Diameter { get; init; }
        public DrainVisiblePartEnum VisiblePart { get; init; }
        public DrainWaterproofingEnum Waterproofing { get; init; }
        public DrainHeatingEnum Heating { get; init; }
        public DrainRenovationEnum Renovation { get; init; }
        public DrainFlapSealEnum FlapSeal { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalDrains { get; set; }

        public IEnumerable<DrainDetailsServiceModel> Drains { get; set; }
    }
}
