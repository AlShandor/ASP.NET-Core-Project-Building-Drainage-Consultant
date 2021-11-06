namespace BuildingDrainageConsultant.Services.SafeDrains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Data.Models.Enums.SafeDrains;
    using BuildingDrainageConsultant.Services.Images;
    using BuildingDrainageConsultant.Services.SafeDrains.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.SafeDrain;
    public class SafeDrainService : ISafeDrainService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;

        public SafeDrainService(BuildingDrainageConsultantDbContext data, IConfigurationProvider mapper, IImageHLService images)
        {
            this.data = data;
            this.mapper = mapper;
            this.images = images;
        }

        public SafeDrainQueryServiceModel All(
            string searchTerm,
            SafeDrainDirectionEnum direction,
            SafeDrainDiameterEnum diameter,
            SafeDrainWaterproofingEnum waterproofing,
            SafeDrainHeatingEnum heating,
            SafeDrainSortingEnum sorting,
            int currentPage,
            int drainsPerPage)
        {
            var safeDrainQuery = this.data.SafeDrains.AsQueryable();

            safeDrainQuery = FilterDrainQueryByParameters(safeDrainQuery, searchTerm, direction, diameter,
                                                 waterproofing, heating);

            safeDrainQuery = SortDrainQueryResults(safeDrainQuery, sorting);

            var totalDrains = safeDrainQuery.Count();

            var drains = safeDrainQuery
                .Skip((currentPage - 1) * drainsPerPage)
                .Take(drainsPerPage)
                .ProjectTo<SafeDrainListingServiceModel>(this.mapper)
                .ToList();

            return new SafeDrainQueryServiceModel
            {
                TotalDrains = totalDrains,
                CurrentPage = currentPage,
                DrainsPerPage = drainsPerPage,
                Drains = drains
            };
        }

        public int Create(
            string name, 
            double flowRateFree, 
            double flowRate3mVertical, 
            int drainageAreaFree, 
            int draingeArea3mVertical, 
            int depth, 
            SafeDrainDirectionEnum direction, 
            SafeDrainDiameterEnum diameter, 
            SafeDrainWaterproofingEnum waterproofing, 
            SafeDrainHeatingEnum heating, 
            int? imageId, 
            string description)
        {
            var drainData = new SafeDrain
            {
                Name = name,
                FlowRateFree = flowRateFree,
                FlowRate3mVertical = flowRate3mVertical,
                DrainageAreaFree = drainageAreaFree,
                DrainageArea3mVertical = draingeArea3mVertical,
                Depth = depth,
                Direction = direction,
                Diameter = diameter,
                Waterproofing = waterproofing,
                Heating = heating,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.SafeDrains.Add(drainData);
            this.data.SaveChanges();

            return drainData.Id;
        }

        public bool Edit(
            int id, 
            string name, 
            double flowRateFree, 
            double flowRate3mVertical, 
            int drainageAreaFree, 
            int draingeArea3mVertical, 
            int depth, 
            SafeDrainDirectionEnum direction, 
            SafeDrainDiameterEnum diameter, 
            SafeDrainWaterproofingEnum waterproofing, 
            SafeDrainHeatingEnum heating, 
            int? imageId, 
            string description)
        {
            var drainData = this.data.SafeDrains.Find(id);

            if (drainData == null)
            {
                return false;
            }

            drainData.Name = name;
            drainData.FlowRateFree = flowRateFree;
            drainData.FlowRate3mVertical = flowRate3mVertical;
            drainData.DrainageAreaFree = drainageAreaFree;
            drainData.DrainageArea3mVertical = draingeArea3mVertical;
            drainData.Depth = depth;
            drainData.Direction = direction;
            drainData.Diameter = diameter;
            drainData.Waterproofing = waterproofing;
            drainData.Heating = heating;
            drainData.ImageId = imageId == null ? DefaultImageId : imageId;
            drainData.Description = description;

            this.data.SaveChanges();

            return true;
        }
        public SafeDrainServiceModel Details(int drainId)
        => this.data
                .SafeDrains
                .Where(d => d.Id == drainId)
                .ProjectTo<SafeDrainServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var safeDrain = this.data.SafeDrains.Find(id);

            if (safeDrain == null)
            {
                return false;
            }

            this.data.SafeDrains.Remove(safeDrain);
            data.SaveChanges();

            return true;
        }

        public IEnumerable<SafeDrainListingServiceModel> ByUser(string userId)
        {
            var user = this.data.Users
                .Where(user => user.Id == userId)
                .Include(d => d.SafeDrains)
                .ThenInclude(d => d.Image)
                .FirstOrDefault();

            var drains = this.GetSafeDrains(user.SafeDrains.AsQueryable());

            return drains;
        }

        public bool AddToMine(string userId, int drainId)
        {
            var safeDrain = this.data.SafeDrains.Find(drainId);

            if (safeDrain == null)
            {
                return false;
            }

            var user = this.data.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            user.SafeDrains.Add(safeDrain);
            data.SaveChanges();

            return true;
        }

        public bool RemoveFromMine(string userId, int drainId)
        {
            var user = this.data.Users
                .Where(u => u.Id == userId)
                .Include(u => u.SafeDrains)
                .FirstOrDefault();

            var safeDrain = user.SafeDrains.FirstOrDefault(d => d.Id == drainId);

            if (user == null || safeDrain == null)
            {
                return false;
            }

            user.SafeDrains.Remove(safeDrain);
            this.data.SaveChanges();

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

        public int GetImageIdByName(string name)
        {
            var drainImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.SafeDrains);

            if (drainImage == null)
            {
                return DefaultImageId;
            }

            return drainImage.Id;
        }

        private IEnumerable<SafeDrainListingServiceModel> GetSafeDrains(IQueryable<SafeDrain> safeDrainQuery)
            => safeDrainQuery
                .ProjectTo<SafeDrainListingServiceModel>(this.mapper)
                .ToList();

        private IQueryable<SafeDrain> FilterDrainQueryByParameters(
            IQueryable<SafeDrain> safeDrainQuery, 
            string searchTerm, 
            SafeDrainDirectionEnum direction, 
            SafeDrainDiameterEnum diameter, 
            SafeDrainWaterproofingEnum waterproofing, 
            SafeDrainHeatingEnum heating)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                safeDrainQuery = safeDrainQuery.Where(d =>
                    d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (direction != 0)
            {
                safeDrainQuery = safeDrainQuery.Where(d => d.Direction == direction);
            }

            if (diameter != 0)
            {
                safeDrainQuery = safeDrainQuery.Where(d => d.Diameter == diameter);
            }

            if (waterproofing != 0)
            {
                safeDrainQuery = safeDrainQuery.Where(d => d.Waterproofing == waterproofing);
            }

            if (heating != 0)
            {
                safeDrainQuery = safeDrainQuery.Where(d => d.Heating == heating);
            }

            return safeDrainQuery;
        }

        private IQueryable<SafeDrain> SortDrainQueryResults(IQueryable<SafeDrain> safeDrainQuery, SafeDrainSortingEnum sorting)
        {
            safeDrainQuery = sorting switch
            {
                SafeDrainSortingEnum.FlowRateAscending => safeDrainQuery.OrderBy(d => d.FlowRateFree),
                SafeDrainSortingEnum.FlowRateDescending => safeDrainQuery.OrderByDescending(d => d.FlowRateFree),
                SafeDrainSortingEnum.DiameterAscending => safeDrainQuery.OrderBy(d => d.Diameter),
                SafeDrainSortingEnum.DiameterDescending => safeDrainQuery.OrderByDescending(d => d.Diameter),
                _ => safeDrainQuery.OrderByDescending(d => d.Id)
            };

            return safeDrainQuery;
        }
    }
}
