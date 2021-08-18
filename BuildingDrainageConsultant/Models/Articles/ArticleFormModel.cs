namespace BuildingDrainageConsultant.Models.Articles
{
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

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}
