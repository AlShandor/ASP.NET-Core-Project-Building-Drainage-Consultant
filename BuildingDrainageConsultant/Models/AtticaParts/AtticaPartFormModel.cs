namespace BuildingDrainageConsultant.Models.AtticaParts
{
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.AtticaPart;
    public class AtticaPartFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHLServiceModel Image { get; set; }

        public IEnumerable<ImageHLServiceModel> Images { get; set; }
    }
}
