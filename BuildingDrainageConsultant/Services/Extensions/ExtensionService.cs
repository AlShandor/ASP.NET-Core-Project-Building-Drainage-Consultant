namespace BuildingDrainageConsultant.Services.Extensions
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using BuildingDrainageConsultant.Services.Images;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.Extension;

    public class ExtensionService : IExtensionService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;

        public ExtensionService(BuildingDrainageConsultantDbContext data, IMapper mapper, IImageHLService images)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.images = images;
        }

        public IEnumerable<ExtensionServiceModel> All(string searchTerm)
        {
            var extensionsQuery = this.data.Extensions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                extensionsQuery = extensionsQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var extensions = extensionsQuery
                .OrderByDescending(p => p.Id)
                .ProjectTo<ExtensionServiceModel>(this.mapper)
                .ToList();

            return extensions;
        }

        public int Create(string name, int? imageId, string description)
        {
            var extensionData = new Extension
            {
                Name = name,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.Extensions.Add(extensionData);
            this.data.SaveChanges();

            return extensionData.Id;
        }

        public bool Edit(int id, string name, int? imageId, string description)
        {
            var extensionData = this.data.Extensions.Find(id);

            if (extensionData == null)
            {
                return false;
            }

            extensionData.Name = name;
            extensionData.ImageId = imageId == null ? DefaultImageId : imageId;
            extensionData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public ExtensionServiceModel Details(int id)
        => this.data
                .Extensions
                .Where(d => d.Id == id)
                .ProjectTo<ExtensionServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var extension = this.data.Extensions.Find(id);

            if (extension == null)
            {
                return false;
            }

            this.data.Extensions.Remove(extension);
            data.SaveChanges();

            return true;
        }

        public int GetImageIdByName(string name)
        {
            var extensionImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.Extensions);

            if (extensionImage == null)
            {
                return DefaultImageId;
            }

            return extensionImage.Id;
        }
    }
}
