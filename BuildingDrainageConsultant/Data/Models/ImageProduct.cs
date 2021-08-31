namespace BuildingDrainageConsultant.Data.Models
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImageProduct
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Path { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Photo is required.")]
        public List<IFormFile> ImageFiles { get; set; }
    }
}
