namespace BuildingDrainageConsultant.Data.Models.Enums.Attica
{
    using System.ComponentModel.DataAnnotations;

    public enum AtticaRoofTypeEnum
    {
        [Display(Name = "Warm Roof")]
        WarmRoof = 1,

        [Display(Name = "Cold Roof")]
        ColdRoof = 2,
    }
}
