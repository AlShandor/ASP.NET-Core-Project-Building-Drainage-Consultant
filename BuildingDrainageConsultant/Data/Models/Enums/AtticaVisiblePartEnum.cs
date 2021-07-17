namespace BuildingDrainageConsultant.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum AtticaVisiblePartEnum
    {
        [Display(Name = "Flat leaf catcher (Warm roof)")]
        FlatLeafCatcherWarmRoof,

        [Display(Name = "Domed leaf catcher (Warm roof)")]
        DomedLeafCatcherWarmRoof,

        [Display(Name = "Leaf catcher (Cold roof)")]
        LeafCatcherColdRoof,

        Grate,

    }
}
