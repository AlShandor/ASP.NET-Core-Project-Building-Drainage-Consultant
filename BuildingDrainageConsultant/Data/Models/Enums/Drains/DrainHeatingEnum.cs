namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainHeatingEnum
    {
        [Display(Name = "No Heating")]
        NoHeating = 1,

        [Display(Name = "With Heating")]
        WithHeating = 2
    }
}
