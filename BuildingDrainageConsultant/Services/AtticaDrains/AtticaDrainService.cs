namespace BuildingDrainageConsultant.Services.AtticaDrains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AtticaDrainService : IAtticaDrainService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        public AtticaDrainService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<AtticaDrainServiceModel> All(string searchTerm)
        {
            var atticaDrainQuery = this.data.AtticaDrains.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                atticaDrainQuery = atticaDrainQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var atticaDrains = atticaDrainQuery
                .ProjectTo<AtticaDrainServiceModel>(this.mapper)
                .ToList();

            return atticaDrains;
        }

        public AtticaDrainServiceModel Details(int id)
        => this.data
                .AtticaDrains
                .Where(d => d.Id == id)
                .ProjectTo<AtticaDrainServiceModel>(this.mapper)
                .FirstOrDefault();

        public int Create(
            string name,
            double flowRate,
            int drainageArea,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart)
        {
            var atticaDrainData = new AtticaDrain
            {
                Name = name,
                FlowRate = flowRate,
                DrainageArea = drainageArea,
                ScreedWaterproofing = screedWaterproofing,
                ConcreteWaterproofing = concreteWaterproofing,
                Diameter = diameter,
                VisiblePart = visiblePart,
                AtticaParts = new List<AtticaPart>()
            };

            this.data.AtticaDrains.Add(atticaDrainData);
            this.data.SaveChanges();

            return atticaDrainData.Id;
        }

        public bool Edit(
            int id,
            string name,
            double flowRate,
            int drainageArea,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart)
        {
            var atticaDrainData = this.data.AtticaDrains.Find(id);

            if (atticaDrainData == null)
            {
                return false;
            }

            atticaDrainData.Name = name;
            atticaDrainData.FlowRate = flowRate;
            atticaDrainData.DrainageArea = drainageArea;
            atticaDrainData.ScreedWaterproofing = screedWaterproofing;
            atticaDrainData.ConcreteWaterproofing = concreteWaterproofing;
            atticaDrainData.Diameter = diameter;
            atticaDrainData.VisiblePart = visiblePart;

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var atticaDrain = this.data.AtticaDrains.Find(id);

            if (atticaDrain == null)
            {
                return false;
            }

            this.data.AtticaDrains.Remove(atticaDrain);
            data.SaveChanges();

            return true;
        }

        public IEnumerable<AtticaDetailServiceModel> GetAtticaDetails()
            => this.data.AtticaDetails
                .AsQueryable()
                .ProjectTo<AtticaDetailServiceModel>(this.mapper)
                .ToList();


        public AtticaDetailServiceModel GetAtticaDetailById(int id)
            => this.data
                .AtticaDetails
                .Where(d => d.Id == id)
                .ProjectTo<AtticaDetailServiceModel>(this.mapper)
                .FirstOrDefault();

        public IEnumerable<AtticaPartServiceModel> GetAtticaParts()
            => this.data.AtticaParts
                .AsQueryable()
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .ToList();

        public AtticaPartServiceModel GetAtticaPartById(int id)
            => this.data
                .AtticaParts
                .Where(d => d.Id == id)
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .FirstOrDefault();
    }
}