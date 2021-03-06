namespace BuildingDrainageConsultant.Services.AtticaDrains
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class AtticaDrainService : IAtticaDrainService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IAtticaPartService atticaParts;

        public AtticaDrainService(BuildingDrainageConsultantDbContext data, IMapper mapper, IAtticaPartService atticaParts)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.atticaParts = atticaParts;
        }

        public AtticaDrainQueryServiceModel All(string searchTerm, int currentPage, int drainsPerPage)
        {
            var atticaDrainQuery = this.data.AtticaDrains.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                atticaDrainQuery = atticaDrainQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalDrains = atticaDrainQuery.Count();

            var atticaDrains = atticaDrainQuery
                .Skip((currentPage - 1) * drainsPerPage)
                .Take(drainsPerPage)
                .ProjectTo<AtticaDrainListingModel>(this.mapper)
                .ToList();

            return new AtticaDrainQueryServiceModel
            {
                TotalDrains = totalDrains,
                CurrentPage = currentPage,
                DrainsPerPage = drainsPerPage,
                AtticaDrains = atticaDrains
            };
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

            atticaDrainQuery = FilterAtticaDrainQueryByParameters(atticaDrainQuery, searchTerm, screedWaterproofing, 
                                                                  concreteWaterproofing, diameter);

            atticaDrainQuery = SortAtticaDrainQueryResults(atticaDrainQuery, sorting);

            var totalDrains = atticaDrainQuery.Count();

            var atticaDrains = atticaDrainQuery
                .Skip((currentPage - 1) * drainsPerPage)
                .Take(drainsPerPage)
                .ProjectTo<AtticaDrainListingModel>(this.mapper)
                .ToList();

            return new AtticaDrainQueryServiceModel
            {
                TotalDrains = totalDrains,
                CurrentPage = currentPage,
                DrainsPerPage = drainsPerPage,
                AtticaDrains = atticaDrains
            };
        }

        public int Create(
            int detailId,
            string name,
            double flowRate35mm,
            double flowRate100mm,
            int drainageArea35mm,
            int drainageArea100mm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart)
        {
            var atticaDrainData = new AtticaDrain
            {
                Name = name,
                FlowRate35mm = flowRate35mm,
                FlowRate100mm = flowRate100mm,
                DrainageArea35mm = drainageArea35mm,
                DrainageArea100mm = drainageArea100mm,
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
            double flowRate35mm,
            double flowRate100mm,
            int drainageArea35mm,
            int drainageArea100mm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart)
        {
            var atticaDrainData = this.data.AtticaDrains
                .Where(a => a.Id == id)
                .Include(a => a.AtticaParts)
                .FirstOrDefault();

            if (atticaDrainData == null)
            {
                return false;
            }

            var atticaPartsNames = atticaDrainData.AtticaParts
                .Select(p => p.Name)
                .ToArray();

            atticaDrainData.Name = string.Join(" + ", atticaPartsNames);
            atticaDrainData.FlowRate35mm = flowRate35mm;
            atticaDrainData.FlowRate100mm = flowRate100mm;
            atticaDrainData.DrainageArea35mm = drainageArea35mm;
            atticaDrainData.DrainageArea100mm = drainageArea100mm;
            atticaDrainData.ScreedWaterproofing = screedWaterproofing;
            atticaDrainData.ConcreteWaterproofing = concreteWaterproofing;
            atticaDrainData.Diameter = diameter;
            atticaDrainData.VisiblePart = visiblePart;

            this.data.SaveChanges();

            return true;
        }
        public AtticaDrainServiceModel Details(int id)
        => this.data
                .AtticaDrains
                .Where(d => d.Id == id)
                .ProjectTo<AtticaDrainServiceModel>(this.mapper)
                .FirstOrDefault();

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

        public IEnumerable<AtticaDrainListingModel> ByUser(string userId)
        {
            var user = this.data.Users
                .Where(user => user.Id == userId)
                .Include(d => d.AtticaDrains)
                .ThenInclude(ad => ad.AtticaParts)
                .ThenInclude(p => p.Image)        
                .FirstOrDefault();

            var atticaDrains = this.GetAtticaDrains(user.AtticaDrains.AsQueryable());

            return atticaDrains;
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
                                 .Where(a => a.Id == drainId)
                                 .Include(a => a.AtticaParts)
                                 .SingleOrDefault();

            if (atticaPart == null || drain == null)
            {
                return false;
            }

            if (drain.AtticaParts.Contains(atticaPart))
            {
                return true;
            }


            drain.AtticaParts.Add(atticaPart);
            var atticaPartsNames = drain.AtticaParts.Select(p => p.Name).ToArray();
            drain.Name = string.Join(" + ", atticaPartsNames);

            data.SaveChanges();

            return true;
        }

        public IEnumerable<AtticaPartServiceModel> GetAtticaParts()
            => this.data.AtticaParts
                .AsQueryable()
                .ProjectTo<AtticaPartServiceModel>(this.mapper)
                .ToList();

        public bool RemovePart(int partId, int drainId)
        {
            var drain = this.data.AtticaDrains
                                 .Where(a => a.Id == drainId)
                                 .Include(a => a.AtticaParts)
                                 .SingleOrDefault();

            var part = drain.AtticaParts.FirstOrDefault(p => p.Id == partId);

            if (drain == null || part == null)
            {
                return false;
            }

            drain.AtticaParts.Remove(part);
            var atticaPartsNames = drain.AtticaParts.Select(p => p.Name).ToArray();
            drain.Name = string.Join(" + ", atticaPartsNames);

            this.data.SaveChanges();

            return true;
        }

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
                .Where(u => u.Id == userId)
                .Include(u => u.AtticaDrains)
                .FirstOrDefault();

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

        public ICollection<AtticaPart> GetAtticaPartsFromString(string partsString)
        {
            if (string.IsNullOrEmpty(partsString))
            {
                return null;
            }

            var partsStringCollection = partsString.Split("+").Select(a => a.Trim());
            List<AtticaPart> parts = new();

            foreach (var partName in partsStringCollection)
            {
                var part = this.atticaParts.GetAtticaPartByName(partName);
                if (part != null)
                {
                    parts.Add(part);
                }
            }

            return parts;
        }

        private IEnumerable<AtticaDrainListingModel> GetAtticaDrains(IQueryable<AtticaDrain> drainQuery)
            => drainQuery
                .ProjectTo<AtticaDrainListingModel>(this.mapper)
                .ToList();

        private IQueryable<AtticaDrain> FilterAtticaDrainQueryByParameters(
            IQueryable<AtticaDrain> atticaDrainQuery, 
            string searchTerm,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter)
        {
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

            return atticaDrainQuery;
        }

        private IQueryable<AtticaDrain> SortAtticaDrainQueryResults(IQueryable<AtticaDrain> atticaDrainQuery, AtticaDrainSortingEnum sorting)
        {
            atticaDrainQuery = sorting switch
            {
                AtticaDrainSortingEnum.DiameterAscending => atticaDrainQuery.OrderBy(d => d.Diameter),
                AtticaDrainSortingEnum.DiameterDescending => atticaDrainQuery.OrderByDescending(d => d.Diameter),
                AtticaDrainSortingEnum.FlowRate35mmAscending => atticaDrainQuery.OrderBy(d => d.FlowRate35mm),
                AtticaDrainSortingEnum.FlowRate35mmDescending => atticaDrainQuery.OrderByDescending(d => d.FlowRate35mm),
                AtticaDrainSortingEnum.FlowRate100mmAscending => atticaDrainQuery.OrderBy(d => d.FlowRate100mm),
                AtticaDrainSortingEnum.FlowRate100mmDescending => atticaDrainQuery.OrderByDescending(d => d.FlowRate100mm),
                _ => atticaDrainQuery.OrderByDescending(d => d.Id)
            };

            return atticaDrainQuery;
        }
    }
}