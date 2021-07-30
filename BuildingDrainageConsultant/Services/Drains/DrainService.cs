namespace BuildingDrainageConsultant.Services.Drains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using System.Linq;

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
    }
}
