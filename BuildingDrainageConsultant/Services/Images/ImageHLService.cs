namespace BuildingDrainageConsultant.Services.Images
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Models.Images;
    using BuildingDrainageConsultant.Services.Images.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ImageHLService : IImageHLService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;

        public ImageHLService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public string AddImagesToGallery(ImageHLViewModel model)
        {
            ImageHLCategoriesEnum imageCategory;
            Enum.TryParse(model.ImageCategory, out imageCategory);
            var gallery = model.ImageCategory + "Gallery";

            var files = model.UploadImageFiles;
            if (files == null)
            {
                return gallery;
            }

            if (files.Count > 0)
            {
                foreach (var img in files)
                {
                    ImageHL image = new ImageHL();
                    var filePath = $"wwwroot/images/{model.ImageCategory.ToLower()}/{img.FileName}";
                    var fileName = img.FileName;
                    if (GalleryContainsImage(fileName, model.ImageCategory))
                    {
                        continue;
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        img.CopyTo(stream);
                        image.Name = fileName;
                        image.Path = filePath;
                        image.ImageCategory = imageCategory;
                        data.Images.Add(image);
                    }
                }
            }

            data.SaveChanges();

            return gallery;
        }

        public ImageHLServiceModel GetImageById(int id)
         => this.data.Images
                .Where(i => i.Id == id)
                .ProjectTo<ImageHLServiceModel>(this.mapper)
                .FirstOrDefault();

        public IEnumerable<ImageHLServiceModel> GetDrainImages()
        => this.data.Images
                .Where(i => i.ImageCategory == ImageHLCategoriesEnum.Drains)
                .ProjectTo<ImageHLServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<ImageHLServiceModel> GetAtticaDetailsImages()
        => this.data.Images
                .Where(i => i.ImageCategory == ImageHLCategoriesEnum.AtticaDetails)
                .ProjectTo<ImageHLServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<ImageHLServiceModel> GetAtticaPartsImages()
        => this.data.Images
                .Where(i => i.ImageCategory == ImageHLCategoriesEnum.AtticaParts)
                .ProjectTo<ImageHLServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<ImageHLServiceModel> GetArticlesImages()
        => this.data.Images
                .Where(i => i.ImageCategory == ImageHLCategoriesEnum.Articles)
                .ProjectTo<ImageHLServiceModel>(this.mapper)
                .ToList();

        public bool Delete(int id)
        {
            var image = this.data.Images.Find(id);

            if (image == null)
            {
                return false;
            }

            this.data.Images.Remove(image);
            data.SaveChanges();

            if (System.IO.File.Exists(image.Path))
            {
                System.IO.File.Delete(image.Path);
            }

            return true;
        }
        public string GetImageGallery(int id)
        {
            var image = this.data.Images.Find(id);

            if (image == null)
            {
                return string.Empty;
            }

            return image.ImageCategory.ToString() + "Gallery";
        }

        public void CreateAll(ImageHL[] images)
        {
            foreach (var i in images)
            {
                var image = new ImageHL
                {
                    Name = i.Name,
                    Path = i.Path,
                    ImageCategory = i.ImageCategory
                };

                this.data.Images.Add(image);
            }

            this.data.SaveChanges();
        }

        private bool GalleryContainsImage(string fileName, string imageCategory)
        {
            ImageHLCategoriesEnum category;
            Enum.TryParse(imageCategory, out category);

            var imageNames = this.data.Images
                .Where(i => i.ImageCategory == category)
                .Select(i => i.Name)
                .ToList();

            if (!imageNames.Contains(fileName))
            {
                return false;
            }

            return true;
        }
    }
}
