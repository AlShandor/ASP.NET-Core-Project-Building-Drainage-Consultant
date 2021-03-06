namespace BuildingDrainageConsultant.Services.WaterproofingKits
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Services.Images;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.WaterproofingKit;

    public class WaterproofingKitService : IWaterproofingKitService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;

        public WaterproofingKitService(BuildingDrainageConsultantDbContext data, IMapper mapper, IImageHLService images)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.images = images;
        }

        public IEnumerable<WaterproofingKitServiceModel> All(string searchTerm)
        {
            var waterproofingKitsQuery = this.data.WaterproofingKits.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                waterproofingKitsQuery = waterproofingKitsQuery
                    .Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var waterproofingKits = waterproofingKitsQuery
                .OrderByDescending(w => w.Id)
                .ProjectTo<WaterproofingKitServiceModel>(this.mapper)
                .ToList();

            return waterproofingKits;
        }

        public int Create(string name, int? imageId, string description)
        {
            var waterproofingKitData = new WaterproofingKit
            {
                Name = name,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.WaterproofingKits.Add(waterproofingKitData);
            this.data.SaveChanges();

            return waterproofingKitData.Id;
        }

        public bool Edit(int id, string name, int? imageId, string description)
        {
            var waterproofingKitData = this.data.WaterproofingKits.Find(id);

            if (waterproofingKitData == null)
            {
                return false;
            }

            waterproofingKitData.Name = name;
            waterproofingKitData.ImageId = imageId == null ? DefaultImageId : imageId;
            waterproofingKitData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public WaterproofingKitServiceModel Details(int id)
        => this.data
                .WaterproofingKits
                .Where(d => d.Id == id)
                .ProjectTo<WaterproofingKitServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var waterproofingKit = this.data.WaterproofingKits.Find(id);

            if (waterproofingKit == null)
            {
                return false;
            }

            this.data.WaterproofingKits.Remove(waterproofingKit);
            data.SaveChanges();

            return true;
        }

        public int GetImageIdByName(string name)
        {
            var waterproofingKitImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.WaterproofingKits);

            if (waterproofingKitImage == null)
            {
                return DefaultImageId;
            }

            return waterproofingKitImage.Id;
        }

        public WaterproofingKit GetWaterproofingKit(string kitName)
        {
            var waterproofingKits = this.data.WaterproofingKits.ToList();
            foreach (var kit in waterproofingKits)
            {
                if (string.Equals(kit.Name.ToLower(), kitName.ToLower()))
                {
                    return kit;
                }
            }

            return null;
        }
    }
}
