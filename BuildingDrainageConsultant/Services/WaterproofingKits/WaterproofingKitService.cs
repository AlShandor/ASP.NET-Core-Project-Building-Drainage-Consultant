namespace BuildingDrainageConsultant.Services.WaterproofingKits
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Data.DataConstants.WaterproofingKit;
    public class WaterproofingKitService : IWaterproofingKitService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        public WaterproofingKitService(BuildingDrainageConsultantDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<WaterproofingKitServiceModel> All(string searchTerm)
        {
            var waterproofingKitsQuery = this.data.WaterproofingKits.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                waterproofingKitsQuery = waterproofingKitsQuery
                    .Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var waterproofingKits = waterproofingKitsQuery
                .ProjectTo<WaterproofingKitServiceModel>(this.mapper)
                .ToList();

            return waterproofingKits;
        }

        public int Create(string name, int? imageId, string description)
        {
            var waterproofingKitData = new WaterproofingKit
            {
                Name = name,
                ImageId = imageId == null ? DefaultImageId : imageId,
                Description = description
            };

            this.data.WaterproofingKits.Add(waterproofingKitData);
            this.data.SaveChanges();

            return waterproofingKitData.Id;
        }

        public bool Edit(int id, string name, int? imageId, string description)
        {
            var waterproofingKitData = this.data.WaterproofingKits.Find(id);

            if (waterproofingKitData == null)
            {
                return false;
            }

            waterproofingKitData.Name = name;
            waterproofingKitData.ImageId = imageId == null ? DefaultImageId : imageId;
            waterproofingKitData.Description = description;

            this.data.SaveChanges();

            return true;
        }

        public WaterproofingKitServiceModel Details(int id)
        => this.data
                .WaterproofingKits
                .Where(d => d.Id == id)
                .ProjectTo<WaterproofingKitServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool Delete(int id)
        {
            var waterproofingKit = this.data.WaterproofingKits.Find(id);

            if (waterproofingKit == null)
            {
                return false;
            }

            this.data.WaterproofingKits.Remove(waterproofingKit);
            data.SaveChanges();

            return true;
        }

        public void CreateAll(WaterproofingKit[] waterproofingKits)
        {
            foreach (var a in waterproofingKits)
            {
                var waterproofingKit = new WaterproofingKit
                {
                    Name = a.Name,
                    ImageId = a.ImageId,
                    Description = a.Description
                };

                this.data.WaterproofingKits.Add(waterproofingKit);
            }

            this.data.SaveChanges();
        }
    }
}
