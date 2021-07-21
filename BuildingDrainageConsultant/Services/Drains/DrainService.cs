namespace BuildingDrainageConsultant.Services.Drains
{
    using BuildingDrainageConsultant.Data;
    using System.Linq;

    public class DrainService : IDrainService
    {
        private readonly BuildingDrainageConsultantDbContext data;

        public DrainService(BuildingDrainageConsultantDbContext data)
            => this.data = data;

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
                .Select(d => new DrainServiceModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ImageUrl = d.ImageUrl,
                })
                .ToList();

            return new DrainQueryServiceModel
            {
                TotalDrains = totalDrains,
                CurrentPage = currentPage,
                DrainsPerPage = drainsPerPage,
                Drains = drains
            };
        }
    }
}
