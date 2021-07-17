namespace BuildingDrainageConsultant.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainVisiblePartEnum
    {
        Grate,

        [Display(Name = "Leaf Catcher")]
        LeafCatcher,

        [Display(Name = "Tilable Frame")]
        TilableFrame
    }
}
