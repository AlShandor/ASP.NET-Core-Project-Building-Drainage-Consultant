namespace BuildingDrainageConsultant.Services.Accessories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Accessories.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.Accessory;
    public class AccessoryService : IAccessoryService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        public AccessoryService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<AccessoryServiceModel> All(string searchTerm)
        {
            var accessoriesQuery = this.data.Accessories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                accessoriesQuery = accessoriesQuery.Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var accessories = accessoriesQuery
                .OrderByDescending(a => a.Id)
                .ProjectTo<AccessoryServiceModel>(this.mapper)
                .ToList();

            return accessories;
        }

        public int Create(string name, int? imageId, string description)
        {
            var accessoryData = new Accessory
            {
                Name = name,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.Accessories.Add(accessoryData);
            this.data.SaveChanges();

            return accessoryData.Id;
        }

        public bool Edit(int id, string name, int? imageId, string description)
        {
            var accessoryData = this.data.Accessories.Find(id);

            if (accessoryData == null)
            {
                return false;
            }

            accessoryData.Name = name;
            accessoryData.ImageId = imageId == null ? DefaultImageId : imageId;
            accessoryData.Description = description;

            this.data.SaveChanges();

            return true;
        }
        public AccessoryServiceModel Details(int id)
        => this.data
                .Accessories
                .Where(d => d.Id == id)
                .ProjectTo<AccessoryServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var accessory = this.data.Accessories.Find(id);

            if (accessory == null)
            {
                return false;
            }

            this.data.Accessories.Remove(accessory);
            data.SaveChanges();

            return true;
        }

        public void CreateAll(Accessory[] accessories)
        {
            foreach (var a in accessories)
            {
                var accessory = new Accessory
                {
                    Name = a.Name,
                    ImageId = a.ImageId,
                    Description = a.Description
                };

                this.data.Accessories.Add(accessory);
            }

            this.data.SaveChanges();
        }
    }
}
