namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using System;
    using System.IO;
    using System.Linq;

    public class ImageSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            SeedDefaultImage(dbContext);
            SeedCategoryImages(dbContext, "Articles");
            SeedCategoryImages(dbContext, "Accessories");
            SeedCategoryImages(dbContext, "AtticaDetails");
            SeedCategoryImages(dbContext, "AtticaParts");
            SeedCategoryImages(dbContext, "Drains");
            SeedCategoryImages(dbContext, "SafeDrains");
            SeedCategoryImages(dbContext, "Extensions");
            SeedCategoryImages(dbContext, "WaterproofingKits");
        }

        private void SeedDefaultImage(BuildingDrainageConsultantDbContext dbContext)
        {
            ImageHL image = new ImageHL();
            image.Name = "defaultImage.gif";
            image.Path = $"wwwroot/images/common/{image.Name}";
            image.ImageCategory = ImageHLCategoriesEnum.Common;

            dbContext.Images.Add(image);
            dbContext.SaveChanges();
        }

        private void SeedCategoryImages(BuildingDrainageConsultantDbContext dbContext, string imageCategory)
        {
            ImageHLCategoriesEnum category;
            Enum.TryParse(imageCategory, out category);

            DirectoryInfo dirInfo = new DirectoryInfo($"wwwroot/images/{imageCategory.ToLower()}/");
            var files = dirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                ImageHL image = new ImageHL();
                image.Name = file.Name;
                image.Path = $"wwwroot/images/{imageCategory.ToLower()}/{file.Name}";
                image.ImageCategory = category;
                dbContext.Images.Add(image);
            }

            dbContext.SaveChanges();
        }
    }
}
