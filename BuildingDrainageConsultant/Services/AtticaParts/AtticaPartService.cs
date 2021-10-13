namespace BuildingDrainageConsultant.Services.AtticaParts
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using BuildingDrainageConsultant.Services.Images;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.AtticaPart;
    public class AtticaPartService : IAtticaPartService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IImageHLService images;

        public AtticaPartService(BuildingDrainageConsultantDbContext data, IMapper mapper, IImageHLService images)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
            this.images = images;
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

        public int GetImageIdByName(string name)
        {
            var atticaPartImage = this.images.GetImageIdByNameAndCategory(name, ImageHLCategoriesEnum.AtticaParts);

            if (atticaPartImage == null)
            {
                return DefaultImageId;
            }

            return atticaPartImage.Id;
        }

        public AtticaPart GetAtticaPartByName(string name)
        {
            var parts = this.data.AtticaParts.ToList();

            foreach (var part in parts)
            {
                if (string.Equals(part.Name.ToLower(), name.ToLower()))
                {
                    return part;
                }
            }

            return null;
        }
    }
}