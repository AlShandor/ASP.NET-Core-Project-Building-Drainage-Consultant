namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainLoadClassEnum
    {
        [Display(Name = "No Load Class")]
        NoLoadClass = 1,

        [Display(Name = "K3 (max 300 kg)")]
        K3_300kg = 2,

        [Display(Name = "A15 (max 1500 kg)")]
        A15_1500kg = 3,

        [Display(Name = "B125 (max 12500 kg)")]
        B125_12500kg = 4
    }
}
