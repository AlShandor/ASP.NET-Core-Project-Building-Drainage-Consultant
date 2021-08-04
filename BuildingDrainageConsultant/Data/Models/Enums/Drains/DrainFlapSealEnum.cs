namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;
    public enum DrainFlapSealEnum
    {
        [Display(Name = "With Flap Seal")]
        WithFlapSeal = 1,


        [Display(Name = "No Flap Seal")]
        NoFlapSeal = 2
    }
}
