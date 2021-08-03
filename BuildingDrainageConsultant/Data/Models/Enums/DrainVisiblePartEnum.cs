namespace BuildingDrainageConsultant.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainVisiblePartEnum
    {
        Grate = 1,

        [Display(Name = "Leaf Catcher")]
        LeafCatcher = 2,

        [Display(Name = "Tilable Frame")]
        TilableFrame = 3
    }
}
