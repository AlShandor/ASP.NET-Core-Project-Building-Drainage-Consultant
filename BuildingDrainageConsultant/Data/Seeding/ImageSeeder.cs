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

            var articleService = serviceProvider.GetRequiredService<IImageHLService>();

            var articles = new ImageHL[]
            {
                new ImageHL
                {
                    Name = "defaultImage.gif",
                    Path = "wwwroot/images/common/defaultImage.gif",
                    ImageCategory = ImageHLCategoriesEnum.Common
                },
            };

            articleService.CreateAll(articles);
        }
    }
}
