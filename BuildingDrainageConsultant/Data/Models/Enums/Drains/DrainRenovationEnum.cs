namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;
    public enum DrainRenovationEnum
    {
        [Display(Name = "Not for Renovation")]
        NoRenovation = 1,

        [Display(Name = "For Renovation")]
        ForRenovation = 2
    }
}
