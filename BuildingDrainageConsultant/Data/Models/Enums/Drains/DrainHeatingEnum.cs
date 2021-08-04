namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainHeatingEnum
    {
        [Display(Name = "With Heating")]
        WithHeating = 1,

        [Display(Name = "No Heating")]
        NoHeating = 2
    }
}
