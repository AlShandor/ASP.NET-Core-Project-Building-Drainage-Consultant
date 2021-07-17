namespace BuildingDrainageConsultant.Models.Drains
{
    using System.ComponentModel.DataAnnotations;
    public class AddDrainFormModel
    {
        public string Name { get; set; }


        [Display(Name = "Flow Rate")]
        public double FlowRate { get; init; }


        [Display(Name = "Drainage Area")]
        public int DrainageArea { get; init; }

        public string Diameter { get; init; }


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

    }
}
