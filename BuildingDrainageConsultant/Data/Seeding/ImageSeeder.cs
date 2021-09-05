namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class ImageSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            var imageService = serviceProvider.GetRequiredService<IImageHLService>();

            var images = new ImageHL[]
            {
                new ImageHL
                {
                    Name = "defaultImage.gif",
                    Path = "wwwroot/images/common/defaultImage.gif",
                    ImageCategory = ImageHLCategoriesEnum.Common
                },
                new ImageHL
                {
                    Name = "article1.png",
                    Path = "wwwroot/images/articles/article1.png",
                    ImageCategory = ImageHLCategoriesEnum.Articles
                },
                new ImageHL
                {
                    Name = "article2.png",
                    Path = "wwwroot/images/articles/article2.png",
                    ImageCategory = ImageHLCategoriesEnum.Articles
                },
                new ImageHL
                {
                    Name = "article3.png",
                    Path = "wwwroot/images/articles/article3.png",
                    ImageCategory = ImageHLCategoriesEnum.Articles
                },
            };

            imageService.CreateAll(images);
        }
    }
}
