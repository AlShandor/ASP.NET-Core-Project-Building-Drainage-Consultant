namespace BuildingDrainageConsultant.Data.Models.Enums.Drains
{
    using System.ComponentModel.DataAnnotations;
    public enum DrainDirectionEnum
    {
        Horizontal = 1,

        Vertical = 2,

        [Display(Name = "Horizontal/Vertical")]
        HorizontalVertical = 3
    }
}
