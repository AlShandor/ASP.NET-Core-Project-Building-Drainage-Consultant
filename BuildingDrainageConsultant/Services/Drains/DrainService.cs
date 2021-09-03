namespace BuildingDrainageConsultant.Services.Drains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.Drain;
    public class DrainService : IDrainService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;

        public DrainService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
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

        public IEnumerable<DrainDetailsServiceModel> ByUser(string userId)
        {
            var user = this.data.Users
                .Include(d => d.Drains)
                .Where(user => user.Id == userId)
                .FirstOrDefault();

            var drains = this.GetDrains(user.Drains.AsQueryable());

            return drains;
        }

        public DrainServiceModel Details(int drainId)
            => this.data
                .Drains
                .Where(d => d.Id == drainId)
                .ProjectTo<DrainServiceModel>(this.mapper)
                .FirstOrDefault();


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

        public void CreateAll(Drain[] drains)
        {
            foreach (var d in drains)
            {
                var drain = new Drain
                {
                    Name = d.Name,
                    FlowRate = d.FlowRate,
                    DrainageArea = d.DrainageArea,
                    Depth = d.Depth,
                    Direction = d.Direction,
                    Diameter = d.Diameter,
                    VisiblePart = d.VisiblePart,
                    Waterproofing = d.Waterproofing,
                    Heating = d.Heating,
                    Renovation = d.Renovation,
                    FlapSeal = d.FlapSeal,
                    ImageId = d.ImageId,
                    Description = d.Description
                };

                this.data.Drains.Add(drain);
            }

            this.data.SaveChanges();
        }

        private IEnumerable<DrainDetailsServiceModel> GetDrains(IQueryable<Drain> drainQuery)
            => drainQuery
                .ProjectTo<DrainDetailsServiceModel>(this.mapper)
                .ToList();
    }
}