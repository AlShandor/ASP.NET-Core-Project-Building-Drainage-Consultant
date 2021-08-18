namespace BuildingDrainageConsultant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Article;
    public class Article
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
