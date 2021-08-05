namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;
    public enum DrainFlapSealEnum
    {
        [Display(Name = "No Flap Seal")]
        NoFlapSeal = 1,

        [Display(Name = "With Flap Seal")]
        WithFlapSeal = 2

    }
}
