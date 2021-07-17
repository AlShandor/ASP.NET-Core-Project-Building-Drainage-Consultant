namespace BuildingDrainageConsultant.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DrainWaterproofingEnum
    {
        Bitumen,
        TPO,
        PVC,

        [Display(Name = "Flexible membranes up to 2mm")]
        FlexibleMembrane2mm,

        [Display(Name = "Cement-based waterproofing, epoxy resins etc.")]

        LiquidWaterproofing
    }
}
