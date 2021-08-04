namespace BuildingDrainageConsultant.Data.Models.Enums.Attica
{
    using System.ComponentModel.DataAnnotations;
    public enum AtticaWalkableEnum
    {
        Walkable = 1,

        [Display(Name = "Not Walkable")]
        NotWalkable = 2
    }
}
