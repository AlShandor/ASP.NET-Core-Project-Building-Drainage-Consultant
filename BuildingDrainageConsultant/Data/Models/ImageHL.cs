namespace BuildingDrainageConsultant.Data.Models
{
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Image;
    public class ImageHL
    {
        [Key]
        public int Id { get; set; }

        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [StringLength(PathMaxLength)]
        public string Path { get; set; }

        public ImageHLCategoriesEnum ImageCategory { get; set; }

        public ICollection<Drain> Drains { get; set; }

        public ICollection<SafeDrain> SafeDrains { get; set; }

        public ICollection<AtticaDetail> AtticaDetails { get; set; }

        public ICollection<AtticaPart> AtticaParts { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<WaterproofingKit> WaterproofingKits { get; set; }

        public ICollection<Accessory> Accessories { get; set; }

        public ICollection<Extension> Extensions { get; set; }
    }
}
