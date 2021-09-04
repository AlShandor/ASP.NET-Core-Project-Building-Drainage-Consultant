namespace BuildingDrainageConsultant.Models.Articles
{
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Article;
    public class ArticleFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ContentMaxLength)]
        public string Content { get; set; }

        public int? ImageId { get; set; }

        public ImageHLServiceModel Image { get; set; }

        public IEnumerable<ImageHLServiceModel> Images { get; set; }
    }
}
