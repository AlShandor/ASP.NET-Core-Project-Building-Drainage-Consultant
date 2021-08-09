namespace BuildingDrainageConsultant.Data.Models.Enums.Attica
{
    using System.ComponentModel.DataAnnotations;
    public enum AtticaVisiblePartEnum
    {
        [Display(Name = "Flat leaf catcher (Warm roof)")]
        FlatLeafCatcherWarmRoof = 1,

        [Display(Name = "Domed leaf catcher (Warm roof)")]
        DomedLeafCatcherWarmRoof = 2,

        [Display(Name = "Leaf catcher (Cold roof)")]
        LeafCatcherColdRoof = 3,

        Grate = 4,
    }
}
