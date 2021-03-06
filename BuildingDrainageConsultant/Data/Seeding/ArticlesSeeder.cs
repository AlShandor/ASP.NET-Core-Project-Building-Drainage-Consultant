namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Articles;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class ArticlesSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            var articleService = serviceProvider.GetRequiredService<IArticleService>();

            var articles = new Article[]
            {
                new Article
                {
                    Title = "New HL541 Product",
                    Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has",
                    ImageId = 2
                },
                new Article
                {
                    Title = "Best Drainge Systems",
                    Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has",
                    ImageId = 3
                },
                new Article
                {
                    Title = "Renovate your bathroom!",
                    Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has",
                    ImageId = 4
                }
            };

            articleService.CreateAll(articles);
        }
    }
}
