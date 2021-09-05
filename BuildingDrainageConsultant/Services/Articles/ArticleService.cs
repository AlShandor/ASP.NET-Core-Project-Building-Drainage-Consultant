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
        public IEnumerable<ArticleServiceModel> All()
        {
            var articlesQuery = this.data.Articles.AsQueryable();

            var articles = articlesQuery
                .OrderByDescending(a => a.Id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .ToList();

            return articles;
        }

        public int Create(string title, string content, int? imageId)
        {
            var articleData = new Article
            {
                Title = title,
                Content = content,
                ImageId = imageId == null ? DefaultImageId : imageId
            };

            this.data.Articles.Add(articleData);
            this.data.SaveChanges();

            return articleData.Id;
        }

        public bool Edit(int id, string title, string content, int? imageId)
        {
            var articleData = this.data.Articles.Find(id);

            if (articleData == null)
            {
                return false;
            }

            articleData.Title = title;
            articleData.Content = content;
            articleData.ImageId = imageId == null ? DefaultImageId : imageId;

            this.data.SaveChanges();

            return true;
        }
        public ArticleServiceModel Details(int id)
        => this.data
                .Articles
                .Where(d => d.Id == id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .FirstOrDefault();

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

        public IEnumerable<ArticleServiceModel> Latest()
            => this.data
                .Articles
                .OrderByDescending(c => c.Id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public void CreateAll(Article[] articles)
        {
            foreach (var a in articles)
            {
                var article = new Article
                {
                    Title = a.Title,
                    Content = a.Content,
                    ImageId = a.ImageId
                };

                this.data.Articles.Add(article);
            }

            this.data.SaveChanges();
        }
    }
}
