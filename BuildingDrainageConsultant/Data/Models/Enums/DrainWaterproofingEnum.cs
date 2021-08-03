namespace BuildingDrainageConsultant.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainWaterproofingEnum
    {
        Bitumen = 1,

        TPO = 2,

        PVC = 3,

        [Display(Name = "Flexible membranes up to 2mm")]
        FlexibleMembrane2mm = 4,

        [Display(Name = "Cement-based waterproofing, epoxy resins, etc.")]

        LiquidWaterproofing = 5
    }
}
