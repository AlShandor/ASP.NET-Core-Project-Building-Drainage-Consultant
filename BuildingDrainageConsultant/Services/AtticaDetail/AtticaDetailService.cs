﻿namespace BuildingDrainageConsultant.Services.AtticaDetail
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using System.Linq;

    using static Data.DataConstants.AtticaDetail;
    public class AtticaDetailService : IAtticaDetailService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        public AtticaDetailService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public AtticaDetailQueryServiceModel All(AtticaRoofTypeEnum roofType, AtticaWalkableEnum isWalkable)
        {
            var atticaDetailQuery = this.data.AtticaDetails.AsQueryable();

            if (roofType != 0)
            {
                atticaDetailQuery = atticaDetailQuery.Where(d => d.RoofType == roofType);
            }

            if (isWalkable != 0)
            {
                atticaDetailQuery = atticaDetailQuery.Where(d => d.IsWalkable == isWalkable);
            }

            var totalAtticaDetails = atticaDetailQuery.Count();

            var atticaDetails = atticaDetailQuery
                .ProjectTo<AtticaDetailServiceModel>(this.mapper)
                .ToList();

            return new AtticaDetailQueryServiceModel
            {
                TotalDrains = totalAtticaDetails,
                AtticaDetails = atticaDetails
            };
        }

        public int Create(
            AtticaRoofTypeEnum roofType, 
            AtticaWalkableEnum isWalkable, 
            AtticaScreedWaterproofingEnum screedWaterproofing, 
            AtticaVisiblePartEnum visiblePart, 
            string description, 
            string imageUrl)
        {
            var atticaDetailData = new AtticaDetail
            {
                RoofType = roofType,
                IsWalkable = isWalkable,
                ScreedWaterproofing = screedWaterproofing,
                VisiblePart = visiblePart,
                Description = description,
                ImageUrl = imageUrl == null ? DefaultImageUrl : imageUrl
            };


            this.data.AtticaDetails.Add(atticaDetailData);
            this.data.SaveChanges();

            return atticaDetailData.Id;
        }

        public bool Edit(int id, AtticaRoofTypeEnum roofType, AtticaWalkableEnum isWalkable, AtticaScreedWaterproofingEnum screedWaterproofing, AtticaVisiblePartEnum visiblePart, string description, string imageUrl)
        {
            var atticaDetailData = this.data.AtticaDetails.Find(id);

            if (atticaDetailData == null)
            {
                return false;
            }

            atticaDetailData.RoofType = roofType;
            atticaDetailData.IsWalkable = isWalkable;
            atticaDetailData.ScreedWaterproofing = screedWaterproofing;
            atticaDetailData.VisiblePart = visiblePart;
            atticaDetailData.Description = description;
            atticaDetailData.ImageUrl = imageUrl;

            this.data.SaveChanges();

            return true;
        }

        public AtticaDetailServiceModel Details(int id)
        => this.data
                .AtticaDetails
                .Where(d => d.Id == id)
                .ProjectTo<AtticaDetailServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var atticaDetail = this.data.AtticaDetails.Find(id);

            if (atticaDetail == null)
            {
                return false;
            }

            this.data.AtticaDetails.Remove(atticaDetail);
            data.SaveChanges();

            return true;
        }
    }
}