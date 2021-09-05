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
            var atticaPartsQuery = this.data.AtticaParts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                atticaPartsQuery = atticaPartsQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var atticaParts = atticaPartsQuery
                .OrderByDescending(p => p.Id)
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .ToList();

            return atticaParts;
        }

        public int Create(string name, int? imageId, string description)
        {
            var atticaPartData = new AtticaPart
            {
                Name = name,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.AtticaParts.Add(atticaPartData);
            this.data.SaveChanges();

            return atticaPartData.Id;
        }

        public bool Edit(int id, string name, int? imageId, string description)
        {
            var atticaPartData = this.data.AtticaParts.Find(id);

            if (atticaPartData == null)
            {
                return false;
            }

            atticaPartData.Name = name;
            atticaPartData.ImageId = imageId == null ? DefaultImageId : imageId;
            atticaPartData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public AtticaPartServiceModel Details(int id)
        => this.data
                .AtticaParts
                .Where(d => d.Id == id)
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .FirstOrDefault();

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

        public void CreateAll(AtticaPart[] atticaParts)
        {
            foreach (var a in atticaParts)
            {
                var atticaPart = new AtticaPart
                {
                    Name = a.Name,
                    ImageId = a.ImageId,
                    Description = a.Description
                };

                this.data.AtticaParts.Add(atticaPart);
            }

            this.data.SaveChanges();
        }
    }
}