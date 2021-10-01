namespace BuildingDrainageConsultant.Services.Drains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Accessories;
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using BuildingDrainageConsultant.Services.Images;
    using BuildingDrainageConsultant.Services.WaterproofingKits;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.Drain;
    public class DrainService : IDrainService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;
        private readonly IWaterproofingKitService waterproofingKits;
        private readonly IAccessoryService accessories;

        public DrainService(
            BuildingDrainageConsultantDbContext data, 
            IMapper mapper, 
            IImageHLService images,
            IWaterproofingKitService waterproofingKits,
            IAccessoryService accessories)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.images = images;
            this.waterproofingKits = waterproofingKits;
            this.accessories = accessories;
        }

        public DrainQueryServiceModel All(
            string searchTerm,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            DrainSortingEnum sorting,
            int currentPage,
            int drainsPerPage)
        {
            var drainQuery = this.data.Drains.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                drainQuery = drainQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (direction != 0)
            {
                drainQuery = drainQuery.Where(d => d.Direction == direction);
            }

            if (diameter != 0)
            {
                drainQuery = drainQuery.Where(d => d.Diameter == diameter);
            }

            if (visiblePart != 0)
            {
                drainQuery = drainQuery.Where(d => d.VisiblePart == visiblePart);
            }

            if (waterproofing != 0)
            {
                drainQuery = drainQuery.Where(d => d.Waterproofing == waterproofing);
            }

            if (heating != 0)
            {
                drainQuery = drainQuery.Where(d => d.Heating == heating);
            }

            if (renovation != 0)
            {
                drainQuery = drainQuery.Where(d => d.Renovation == renovation);
            }

            if (flapSeal != 0)
            {
                drainQuery = drainQuery.Where(d => d.FlapSeal == flapSeal);
            }


            drainQuery = sorting switch
            {
                DrainSortingEnum.Name => drainQuery.OrderBy(d => d.Name),
                DrainSortingEnum.DepthAscending => drainQuery.OrderBy(d => d.Depth),
                DrainSortingEnum.Waterproofing => drainQuery.OrderBy(d => d.Waterproofing),
                DrainSortingEnum.VisiblePart => drainQuery.OrderBy(d => d.VisiblePart),
                DrainSortingEnum.FlowRateAscending => drainQuery.OrderBy(d => d.FlowRate),
                DrainSortingEnum.FlowRateDescending => drainQuery.OrderByDescending(d => d.FlowRate),
                DrainSortingEnum.DiameterAscending => drainQuery.OrderBy(d => d.Diameter),
                DrainSortingEnum.DiameterDescending => drainQuery.OrderByDescending(d => d.Diameter),
                _ => drainQuery.OrderByDescending(d => d.Id)
            };

            var totalDrains = drainQuery.Count();

            var drains = drainQuery
                .Skip((currentPage - 1) * drainsPerPage)
                .Take(drainsPerPage)
                .ProjectTo<DrainDetailsServiceModel>(this.mapper)
                .ToList();

            return new DrainQueryServiceModel
            {
                TotalDrains = totalDrains,
                CurrentPage = currentPage,
                DrainsPerPage = drainsPerPage,
                Drains = drains
            };
        }

        public int Create(
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            int? imageId,
            string description)
        {
            var drainData = new Drain
            {
                Name = name,
                FlowRate = flowRate,
                DrainageArea = drainageArea,
                Depth = depth,
                Direction = direction,
                Diameter = diameter,
                VisiblePart = visiblePart,
                Waterproofing = waterproofing,
                Heating = heating,
                Renovation = renovation,
                FlapSeal = flapSeal,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.Drains.Add(drainData);
            this.data.SaveChanges();

            return drainData.Id;
        }

        public bool Edit(
            int id,
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            int? imageId,
            string description)
        {
            var drainData = this.data.Drains.Find(id);

            if (drainData == null)
            {
                return false;
            }

            drainData.Name = name;
            drainData.FlowRate = flowRate;
            drainData.DrainageArea = drainageArea;
            drainData.Depth = depth;
            drainData.Direction = direction;
            drainData.Diameter = diameter;
            drainData.VisiblePart = visiblePart;
            drainData.Waterproofing = waterproofing;
            drainData.Heating = heating;
            drainData.Renovation = renovation;
            drainData.FlapSeal = flapSeal;
            drainData.ImageId = imageId == null ? DefaultImageId : imageId;
            drainData.Description = description;

            this.data.SaveChanges();

            return true;
        }
        public DrainServiceModel Details(int drainId)
            => this.data
                .Drains
                .Where(d => d.Id == drainId)
                .ProjectTo<DrainServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var drain = this.data.Drains.Find(id);

            if (drain == null)
            {
                return false;
            }

            this.data.Drains.Remove(drain);
            data.SaveChanges();

            return true;
        }

        public IEnumerable<DrainDetailsServiceModel> ByUser(string userId)
        {
            var user = this.data.Users
                .Include(d => d.Drains)
                .ThenInclude(d => d.Image)
                .Where(user => user.Id == userId)
                .FirstOrDefault();

            var drains = this.GetDrains(user.Drains.AsQueryable());

            return drains;
        }

        public ICollection<WaterproofingKitServiceModel> GetWaterproofingKits()
            => this.data.WaterproofingKits
                .AsQueryable()
                .ProjectTo<WaterproofingKitServiceModel>(this.mapper)
                .ToList();

        public bool AddWaterproofingKit(int waterproofingKitId, int drainId)
        {
            var waterproofingKit = this.data.WaterproofingKits.Find(waterproofingKitId);
            var drain = this.data.Drains
                                 .Include(a => a.WaterproofingKit)
                                 .SingleOrDefault(a => a.Id == drainId);

            if (waterproofingKit == null || drain == null)
            {
                return false;
            }

            if (drain.WaterproofingKitId == waterproofingKitId)
            {
                return true;
            }

            drain.WaterproofingKit = waterproofingKit;
            data.SaveChanges();

            return true;
        }

        public bool RemoveWaterproofingKit(int kitId, int drainId)
        {
            var drain = this.data.Drains
                                 .Include(a => a.WaterproofingKit)
                                 .SingleOrDefault(a => a.Id == drainId);

            var kit = drain.WaterproofingKit;

            if (drain == null || kit == null)
            {
                return false;
            }

            drain.WaterproofingKit = null;
            this.data.SaveChanges();

            return true;
        }

        private IEnumerable<DrainDetailsServiceModel> GetDrains(IQueryable<Drain> drainQuery)
            => drainQuery
                .ProjectTo<DrainDetailsServiceModel>(this.mapper)
                .ToList();

        public ICollection<AccessoryServiceModel> GetAccessories()
        => this.data.Accessories
                .AsQueryable()
                .ProjectTo<AccessoryServiceModel>(this.mapper)
                .ToList();

        public bool AddAccessory(int accessoryId, int drainId)
        {
            var accessory = this.data.Accessories.Find(accessoryId);
            var drain = this.data.Drains
                                 .Include(a => a.Accessories)
                                 .SingleOrDefault(a => a.Id == drainId);

            if (accessory == null || drain == null)
            {
                return false;
            }

            if (drain.Accessories.Contains(accessory))
            {
                return true;
            }

            drain.Accessories.Add(accessory);
            data.SaveChanges();

            return true;
        }

        public bool RemoveAccessory(int accessoryId, int drainId)
        {
            var drain = this.data.Drains
                                 .Include(a => a.Accessories)
                                 .SingleOrDefault(a => a.Id == drainId);

            var accessory = drain.Accessories.FirstOrDefault(a => a.Id == accessoryId);

            if (drain == null || accessory == null)
            {
                return false;
            }

            drain.Accessories.Remove(accessory);
            this.data.SaveChanges();

            return true;
        }

        public ICollection<ExtensionServiceModel> GetExtensions()
        => this.data.Extensions
                .AsQueryable()
                .ProjectTo<ExtensionServiceModel>(this.mapper)
                .ToList();

        public bool AddExtension(int extensionId, int drainId)
        {
            var extension = this.data.Extensions.Find(extensionId);
            var drain = this.data.Drains
                                 .Include(a => a.Extensions)
                                 .SingleOrDefault(a => a.Id == drainId);

            if (extension == null || drain == null)
            {
                return false;
            }

            if (drain.Extensions.Contains(extension))
            {
                return true;
            }

            drain.Extensions.Add(extension);
            data.SaveChanges();

            return true;
        }

        public bool RemoveExtension(int extensionId, int drainId)
        {
            var drain = this.data.Drains
                                 .Include(d => d.Extensions)
                                 .SingleOrDefault(d => d.Id == drainId);

            var extension = drain.Extensions.FirstOrDefault(e => e.Id == extensionId);

            if (drain == null || extension == null)
            {
                return false;
            }

            drain.Extensions.Remove(extension);
            this.data.SaveChanges();

            return true;
        }

        public bool AddToMine(string userId, int drainId)
        {
            var drain = this.data.Drains.Find(drainId);

            if (drain == null)
            {
                return false;
            }

            var user = this.data.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            user.Drains.Add(drain);
            data.SaveChanges();

            return true;
        }

        public bool IsMyDrain(int drainId, string userId)
        {
            var drains = this.ByUser(userId);
            var isMydrain = drains.FirstOrDefault(d => d.Id == drainId);

            if (isMydrain == null)
            {
                return false;
            }
            return true;
        }

        public bool RemoveFromMine(string userId, int drainId)
        {
            var user = this.data.Users
                .Include(u => u.Drains)
                .FirstOrDefault(u => u.Id == userId);

            var drain = user.Drains.FirstOrDefault(d => d.Id == drainId);

            if (user == null || drain == null)
            {
                return false;
            }

            user.Drains.Remove(drain);
            this.data.SaveChanges();

            return true;
        }

        public int GetImageIdByName(string name)
        {
            var drainImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.Drains);

            if (drainImage == null)
            {
                return DefaultImageId;
            }

            return drainImage.Id;
        }

        public int? GetWaterproofingKitId(string kitName)
        {
            var kit = this.waterproofingKits.GetWaterproofingKit(kitName);

            if (kit == null)
            {
                return null;
            }

            return kit.Id;
        }

        public ICollection<Accessory> GetAccessoriesFromString(string accessoriesString)
        {
            if (string.IsNullOrEmpty(accessoriesString))
            {
                return null;
            }

            var accessoriesStringCollection = accessoriesString.Split("+").Select(a => a.Trim());
            List<Accessory> accessories = new();

            foreach (var accName in accessoriesStringCollection)
            {
                var accessory = this.accessories.GetAccessoryByName(accName);
                if (accessory != null)
                {
                    accessories.Add(accessory);
                }
            }

            return accessories;
        }
    }
}