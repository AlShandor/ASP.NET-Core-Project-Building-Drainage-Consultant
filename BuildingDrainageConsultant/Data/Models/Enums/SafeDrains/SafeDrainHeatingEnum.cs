namespace BuildingDrainageConsultant.Data.Models.Enums.SafeDrains
{
    using System.ComponentModel.DataAnnotations;

    public enum SafeDrainHeatingEnum
    {
        [Display(Name = "No Heating")]
        NoHeating = 1,

        [Display(Name = "With Heating")]
        WithHeating = 2
    }
}
