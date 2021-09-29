namespace BuildingDrainageConsultant.Services.Accessories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using BuildingDrainageConsultant.Services.Images;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.Accessory;
    public class AccessoryService : IAccessoryService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;

        public AccessoryService(BuildingDrainageConsultantDbContext data, IMapper mapper, IImageHLService images)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.images = images;
        }

        public IEnumerable<AccessoryServiceModel> All(string searchTerm)
        {
            var accessoriesQuery = this.data.Accessories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                accessoriesQuery = accessoriesQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var accessories = accessoriesQuery
                .OrderByDescending(a => a.Id)
                .ProjectTo<AccessoryServiceModel>(this.mapper)
                .ToList();

            return accessories;
        }

        public int Create(string name, int? imageId, string description)
        {
            var accessoryData = new Accessory
            {
                Name = name,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.Accessories.Add(accessoryData);
            this.data.SaveChanges();

            return accessoryData.Id;
        }

        public bool Edit(int id, string name, int? imageId, string description)
        {
            var accessoryData = this.data.Accessories.Find(id);

            if (accessoryData == null)
            {
                return false;
            }

            accessoryData.Name = name;
            accessoryData.ImageId = imageId == null ? DefaultImageId : imageId;
            accessoryData.Description = description;

            this.data.SaveChanges();

            return true;
        }
        public AccessoryServiceModel Details(int id)
        => this.data
                .Accessories
                .Where(d => d.Id == id)
                .ProjectTo<AccessoryServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var accessory = this.data.Accessories.Find(id);

            if (accessory == null)
            {
                return false;
            }

            this.data.Accessories.Remove(accessory);
            data.SaveChanges();

            return true;
        }

        public int GetImageIdByName(string name)
        {
            var accessoryImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.Accessories);

            if (accessoryImage == null)
            {
                return DefaultImageId;
            }

            return accessoryImage.Id;
        }
    }
}
