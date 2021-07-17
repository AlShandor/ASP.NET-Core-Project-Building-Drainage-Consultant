namespace BuildingDrainageConsultant.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum AtticaRoofTypeEnum
    {
        [Display(Name = "Warm roof")]
        WarmRoof,

        [Display(Name = "Cold roof")]
        ColdRoof,
    }
}
