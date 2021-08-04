namespace BuildingDrainageConsultant.Services.Drains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
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
            int currentPage,
            int drainsPerPage)
        {
            var drainQuery = this.data.Drains.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                drainQuery = drainQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalDrains = drainQuery.Count();

            var drains = drainQuery
                .OrderByDescending(d => d.Id)
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

        public DrainServiceModel Details(int id)
            => this.data
                .Drains
                .Where(c => c.Id == id)
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
    }
}
