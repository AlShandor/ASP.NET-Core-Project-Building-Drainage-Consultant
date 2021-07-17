namespace BuildingDrainageConsultant.Models.Drains
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class AddDrainFormModel
    {
        public string Name { get; set; }

        [Display(Name = "Flow Rate [l/s]")]
        public double FlowRate { get; init; }

        [Display(Name = "Drainage Area [m³]")]
        public int DrainageArea { get; init; }

        [Display(Name = "Visible Part")]
        public string VisiblePart { get; init; }

        public string Waterproofing { get; init; }

        [Display(Name = "Heating")]
        public string HasHeating { get; init; }

        [Display(Name = "For Renovation")]
        public string ForRenovation { get; init; }

        [Display(Name = "Flap Seal")]
        public string HasFlapSeal { get; init; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [BindProperty]
        public string Gender { get; set; }
        public string[] Genders = new[] { "Male", "Female", "Unspecified" };
    }
}
