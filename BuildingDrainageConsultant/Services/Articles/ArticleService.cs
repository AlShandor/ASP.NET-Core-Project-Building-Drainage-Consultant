namespace BuildingDrainageConsultant.Services.Articles
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Articles.Models;
    using System.Collections.Generic;
    using System.Linq;
    using static Data.DataConstants.Article;

    public class ArticleService : IArticleService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;

        public ArticleService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public int Create(string title, string content, string imageUrl)
        {
            var articleData = new Article
            {
                Title = title,
                Content = content,
                ImageUrl = imageUrl == null ? DefaultImageUrl : imageUrl
            };

            this.data.Articles.Add(articleData);
            this.data.SaveChanges();

            return articleData.Id;
        }

        public IEnumerable<ArticleServiceModel> All()
        {
            var articlesQuery = this.data.Articles.AsQueryable();

            var articles = articlesQuery
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .ToList();

            return articles;
        }

        public ArticleServiceModel Details(int id)
        => this.data
                .Articles
                .Where(d => d.Id == id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Edit(int id, string title, string content, string imageUrl)
        {
            var articleData = this.data.Articles.Find(id);

            if (articleData == null)
            {
                return false;
            }

            articleData.Title = title;
            articleData.Content = content;
            articleData.ImageUrl = imageUrl;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var article = this.data.Articles.Find(id);

            if (article == null)
            {
                return false;
            }

            this.data.Articles.Remove(article);
            data.SaveChanges();

            return true;
        }
    }
}
