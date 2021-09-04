using BuildingDrainageConsultant.Data.Models;

namespace BuildingDrainageConsultant.Services.Articles.Models
{
    public class ArticleServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}
