namespace BuildingDrainageConsultant.Services.AtticaDrains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using Microsoft.EntityFrameworkCore;
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

        public AtticaDrainQueryServiceModel SearchAtticaDrains(
            int atticaDetailId,
            string searchTerm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaDrainSortingEnum sorting,
            int currentPage,
            int drainsPerPage)
        {
            var atticaDrainQuery = this.data.AtticaDrains.AsQueryable();

            atticaDrainQuery = atticaDrainQuery.Where(d => d.AtticaDetailId == atticaDetailId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                atticaDrainQuery = atticaDrainQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (screedWaterproofing != 0)
            {
                atticaDrainQuery = atticaDrainQuery.Where(d => d.ScreedWaterproofing == screedWaterproofing);
            }

            if (concreteWaterproofing != 0)
            {
                atticaDrainQuery = atticaDrainQuery.Where(d => d.ConcreteWaterproofing == concreteWaterproofing);
            }

            if (diameter != 0)
            {
                atticaDrainQuery = atticaDrainQuery.Where(d => d.Diameter == diameter);
            }

            atticaDrainQuery = sorting switch
            {
                AtticaDrainSortingEnum.DiameterAscending => atticaDrainQuery.OrderBy(d => d.Diameter),
                AtticaDrainSortingEnum.DiameterDescending => atticaDrainQuery.OrderByDescending(d => d.Diameter),
                AtticaDrainSortingEnum.FlowRateAscending => atticaDrainQuery.OrderBy(d => d.FlowRate),
                AtticaDrainSortingEnum.FlowRateDescending => atticaDrainQuery.OrderByDescending(d => d.FlowRate),
                _ => atticaDrainQuery.OrderByDescending(d => d.Id)
            };

            var totalDrains = atticaDrainQuery.Count();

            var atticaDrains = atticaDrainQuery
                .Skip((currentPage - 1) * drainsPerPage)
                .Take(drainsPerPage)
                .ProjectTo<AtticaDrainServiceModel>(this.mapper)
                .ToList();

            return new AtticaDrainQueryServiceModel
            {
                TotalDrains = totalDrains,
                CurrentPage = currentPage,
                DrainsPerPage = drainsPerPage,
                AtticaDrains = atticaDrains
            };
        }

        public IEnumerable<AtticaDrainServiceModel> ByUser(string userId)
        {
            var user = this.data.Users
                .Include(d => d.AtticaDrains)
                .ThenInclude(ad => ad.AtticaParts)
                .Where(user => user.Id == userId)
                .FirstOrDefault();

            var atticaDrains = this.GetAtticaDrains(user.AtticaDrains.AsQueryable());

            return atticaDrains;
        }

        public AtticaDrainServiceModel Details(int id)
        => this.data
                .AtticaDrains
                .Where(d => d.Id == id)
                .ProjectTo<AtticaDrainServiceModel>(this.mapper)
                .FirstOrDefault();

        public int Create(
            int detailId,
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

            var atticaDetail = this.data.AtticaDetails.Find(detailId);
            atticaDetail.AtticaDrains.Add(atticaDrainData);

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

        public IEnumerable<AtticaPartServiceModel> GetAtticaPartsForDrain(int id)
        => this.data.AtticaDrains
                .Find(id)
                .AtticaParts
                .AsQueryable()
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .ToList();

        public AtticaPartServiceModel GetAtticaPartById(int id)
            => this.data
                .AtticaParts
                .Where(d => d.Id == id)
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool AddAtticaPart(int partId, int drainId)
        {
            var atticaPart = this.data.AtticaParts.Find(partId);
            var drain = this.data.AtticaDrains
                                 .Include(a => a.AtticaParts)
                                 .SingleOrDefault(a => a.Id == drainId);

            if (atticaPart == null || drain == null)
            {
                return false;
            }

            if (drain.AtticaParts.Contains(atticaPart))
            {
                return true;
            }

            drain.AtticaParts.Add(atticaPart);
            data.SaveChanges();

            return true;
        }

        public IEnumerable<AtticaPartServiceModel> GetAtticaParts()
            => this.data.AtticaParts
                .AsQueryable()
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .ToList();

        public bool AddToMine(string userId, int drainId)
        {
            var atticaDrain = this.data.AtticaDrains.Find(drainId);

            if (atticaDrain == null)
            {
                return false;
            }

            var user = this.data.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            user.AtticaDrains.Add(atticaDrain);
            data.SaveChanges();

            return true;
        }

        public bool RemoveFromMine(string userId, int atticaDrainId)
        {
            var user = this.data.Users
                .Include(u => u.AtticaDrains)
                .FirstOrDefault(u => u.Id == userId);

            var atticaDrain = user.AtticaDrains.FirstOrDefault(d => d.Id == atticaDrainId);

            if (user == null || atticaDrain == null)
            {
                return false;
            }

            user.AtticaDrains.Remove(atticaDrain);
            this.data.SaveChanges();

            return true;
        }

        public bool IsMyAtticaDrain(int drainId, string userId)
        {
            var atticaDrains = this.ByUser(userId);
            var isMyAtticaDrain = atticaDrains.FirstOrDefault(d => d.Id == drainId);

            if (isMyAtticaDrain == null)
            {
                return false;
            }
            return true;
        }

        // Seed method
        public void CreateAll(AtticaDrain[] atticaDrains)
        {
            foreach (var a in atticaDrains)
            {
                var atticaDrain = new AtticaDrain
                {
                    Name = a.Name,
                    FlowRate = a.FlowRate,
                    DrainageArea = a.DrainageArea,
                    ScreedWaterproofing = a.ScreedWaterproofing,
                    ConcreteWaterproofing = a.ConcreteWaterproofing,
                    Diameter = a.Diameter,
                    VisiblePart = a.VisiblePart,
                    AtticaDetailId = a.AtticaDetailId,
                    AtticaParts = a.AtticaParts
                };

                this.data.AtticaDrains.Add(atticaDrain);
            }

            this.data.SaveChanges();
        }

        public bool RemovePart(int partId, int drainId)
        {
            var drain = this.data.AtticaDrains
                                 .Include(a => a.AtticaParts)
                                 .SingleOrDefault(a => a.Id == drainId);

            var part = drain.AtticaParts.Where(at => at.Id == partId).FirstOrDefault();

            if (drain == null || part == null)
            {
                return false;
            }

            drain.AtticaParts.Remove(part);
            this.data.SaveChanges();


            return true;
        }

        private IEnumerable<AtticaDrainServiceModel> GetAtticaDrains(IQueryable<AtticaDrain> drainQuery)
            => drainQuery
                .ProjectTo<AtticaDrainServiceModel>(this.mapper)
                .ToList();
    }
}