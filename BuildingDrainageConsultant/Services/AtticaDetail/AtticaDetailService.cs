namespace BuildingDrainageConsultant.Services.AtticaDetail
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.Images;
    using System.Linq;

    using static Data.DataConstants.AtticaDetail;
    public class AtticaDetailService : IAtticaDetailService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;

        public AtticaDetailService(BuildingDrainageConsultantDbContext data, IMapper mapper, IImageHLService images)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.images = images;
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
                .OrderByDescending(d => d.Id)
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
            string description, 
            int? imageId)
        {
            var atticaDetailData = new AtticaDetail
            {
                RoofType = roofType,
                IsWalkable = isWalkable,
                ScreedWaterproofing = screedWaterproofing,
                Description = description,
                ImageId = imageId == null ? DefaultImageId : imageId
            };


            this.data.AtticaDetails.Add(atticaDetailData);
            this.data.SaveChanges();

            return atticaDetailData.Id;
        }

        public bool Edit(
            int id, 
            AtticaRoofTypeEnum roofType, 
            AtticaWalkableEnum isWalkable, 
            AtticaScreedWaterproofingEnum screedWaterproofing,
            string description, 
            int? imageId)
        {
            var atticaDetailData = this.data.AtticaDetails.Find(id);

            if (atticaDetailData == null)
            {
                return false;
            }

            atticaDetailData.RoofType = roofType;
            atticaDetailData.IsWalkable = isWalkable;
            atticaDetailData.ScreedWaterproofing = screedWaterproofing;
            atticaDetailData.Description = description;
            atticaDetailData.ImageId = imageId == null ? DefaultImageId : imageId;

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

        public int GetImageIdByName(string name)
        {
            var atticaDetailImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.AtticaDetails);

            if (atticaDetailImage == null)
            {
                return DefaultImageId;
            }

            return atticaDetailImage.Id;
        }
    }
}