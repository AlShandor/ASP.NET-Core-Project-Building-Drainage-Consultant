namespace BuildingDrainageConsultant.Data.Models.Enums.SafeDrains
{
    using System.ComponentModel.DataAnnotations;

    public enum SafeDrainWaterproofingEnum
    {
        Bitumen = 1,

        TPO = 2,

        PVC = 3,

        [Display(Name = "Flexible membranes up to 2mm")]
        FlexibleMembrane2mm = 4,
    }
}
