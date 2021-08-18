namespace BuildingDrainageConsultant.Services.Articles
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Articles.Models;
    using System.Collections.Generic;
    public interface IArticleService
    {
        public int Create(string title, string content, string imageUrl);

        public IEnumerable<ArticleServiceModel> All();

        public ArticleServiceModel Details(int id);

        public bool Edit(int id, string title, string content, string imageUrl);

        public bool Delete(int id);

        public IEnumerable<ArticleServiceModel> Latest();

        public void CreateAll(Article[] articles);
    }
}
