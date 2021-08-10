namespace BuildingDrainageConsultant.Services.AtticaParts
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.AtticaPart;
    public class AtticaPartService : IAtticaPartService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        public AtticaPartService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }
        public IEnumerable<AtticaPartServiceModel> All(string searchTerm)
        {
            var atticaPartQuery = this.data.AtticaParts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                atticaPartQuery = atticaPartQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var atticaParts = atticaPartQuery
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .ToList();

            return atticaParts;
        }

        public AtticaPartServiceModel Details(int id)
        => this.data
                .AtticaParts
                .Where(d => d.Id == id)
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .FirstOrDefault();

        public int Create(string name, string imageUrl, string description)
        {
            var atticaPartData = new AtticaPart
            {
                Name = name,
                ImageUrl = imageUrl == null ? DefaultImageUrl : imageUrl,
                Description = description
            };

            this.data.AtticaParts.Add(atticaPartData);
            this.data.SaveChanges();

            return atticaPartData.Id;
        }

        public bool Edit(int id, string name, string imageUrl, string description)
        {
            var atticaPartData = this.data.AtticaParts.Find(id);

            if (atticaPartData == null)
            {
                return false;
            }

            atticaPartData.Name = name;
            atticaPartData.ImageUrl = imageUrl;
            atticaPartData.Description = description;

            this.data.SaveChanges();

            return true;
        }
        public bool Delete(int id)
        {
            var atticaPart = this.data.AtticaParts.Find(id);

            if (atticaPart == null)
            {
                return false;
            }

            this.data.AtticaParts.Remove(atticaPart);
            data.SaveChanges();

            return true;
        }
    }
}
