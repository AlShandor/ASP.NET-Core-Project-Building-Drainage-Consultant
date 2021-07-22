﻿namespace BuildingDrainageConsultant.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BuildingDrainageConsultant.Data.Models.Enums;

    using static DataConstants.AtticaDetail;
    public class AtticaDetail
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public AtticaRoofTypeEnum RoofType { get; init; }

        [Required]
        public bool IsWalkable { get; init; }

        public DrainWaterproofingEnum ScreedWaterproofing { get; init; }

        public AtticaVisiblePartEnum VisiblePart { get; init; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; init; }

        [Url]
        public string ImageUrl { get; set; } = DefaultImageUrl;

        public IEnumerable<AtticaDrain> AtticaDrains { get; set; } = new List<AtticaDrain>();
    }
}