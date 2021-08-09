namespace BuildingDrainageConsultant.Services.Merchants
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class MerchantService : IMerchantService
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IConfigurationProvider mapper;
        public MerchantService(
            BuildingDrainageConsultantDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<MerchantServiceModel> All()
        {
            var merchantsQuery = this.data.Merchants.AsQueryable();

            var merchantsResult = merchantsQuery
                .OrderByDescending(m => m.City)
                .ProjectTo<MerchantServiceModel>(this.mapper)
                .ToList();

            return merchantsResult;
        }

        public int Create(
            string name, 
            string city, 
            string address, 
            string email, 
            string phone, 
            string website, 
            double? latitude, 
            double? longitude)
        {
            var merchantData = new Merchant()
            {
                Name = name,
                City = city,
                Address = address,
                Email = email,
                Phone = phone,
                Website = website,
                Latitude = latitude,
                Longitude = longitude
            };

            this.data.Merchants.Add(merchantData);
            this.data.SaveChanges();

            return merchantData.Id;
        }

        public bool Delete(int id)
        {
            var merchantToDelete = this.data.Merchants.Find(id);

            if (merchantToDelete == null)
            {
                return false;
            }

            this.data.Merchants.Remove(merchantToDelete);
            data.SaveChanges();

            return true;
        }

        public bool Edit(
            int id, 
            string name, 
            string city, 
            string address, 
            string email, 
            string phone, 
            string website, 
            double? latitude, 
            double? longitude)
        {
            var merchantData = this.data.Merchants.Find(id);

            if (merchantData == null)
            {
                return false;
            }

            merchantData.Name = name;
            merchantData.City = city;
            merchantData.Address = address;
            merchantData.Email = email;
            merchantData.Phone = phone;
            merchantData.Website = website;
            merchantData.Latitude = latitude;
            merchantData.Longitude = longitude;

            this.data.SaveChanges();

            return true;
        }

        public MerchantServiceModel Details(int id)
        => this.data
                .Merchants
                .Where(m => m.Id == id)
                .ProjectTo<MerchantServiceModel>(this.mapper)
                .FirstOrDefault();
    }
}
