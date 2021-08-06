namespace BuildingDrainageConsultant.Services.Drains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
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
        => GetDrains(this.data
                .Drains
                .Where(d
            => d.UserId == userId));

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
            string imageUrl,
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
                ImageUrl = imageUrl == null ? DefaultImageUrl : imageUrl,
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
            string imageUrl,
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
            drainData.ImageUrl = imageUrl;
            drainData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var drainToDelete = this.data.Drains.Find(id);

            if (drainToDelete == null)
            {
                return false;
            }

            this.data.Drains.Remove(drainToDelete);
            data.SaveChanges();

            return true;
        }

        public bool SaveToMine(string userId, int drainId)
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

        private IEnumerable<DrainDetailsServiceModel> GetDrains(IQueryable<Drain> drainQuery)
            => drainQuery
                .ProjectTo<DrainDetailsServiceModel>(this.mapper)
                .ToList();
    }
}
