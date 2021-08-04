namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;
    public enum DrainRenovationEnum
    {
        [Display(Name = "For Renovation")]
        ForRenovation = 1,

        [Display(Name = "Not for Renovation")]
        NoRenovation = 2
    }
}
