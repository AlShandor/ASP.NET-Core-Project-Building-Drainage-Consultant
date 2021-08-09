namespace BuildingDrainageConsultant.Data.Models.Enums.Attica
{
    using System.ComponentModel.DataAnnotations;

    public enum AtticaScreedWaterproofingEnum
    {
        None = 1,

        Bitumen = 2,

        TPO = 3,

        PVC = 4,

        [Display(Name = "Flexible membranes up to 2mm")]
        FlexibleMembrane2mm = 5,

        [Display(Name = "Cement-based waterproofing, epoxy resins, etc.")]

        LiquidWaterproofing = 6
    }
}
