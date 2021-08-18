namespace BuildingDrainageConsultant.Services.Merchants
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using System.Collections.Generic;

    public interface IMerchantService
    {
        public IEnumerable<MerchantServiceModel> All();

        public int Create(
            string name, 
            string city, 
            string address,
            string email,
            string phone,
            string website,
            double? latitude,
            double? longitude);

        public bool Edit(
            int id,
            string name,
            string city,
            string address,
            string email,
            string phone,
            string website,
            double? latitude,
            double? longitude);

        public MerchantServiceModel Details(int id);

        public bool Delete(int id);

        public void CreateAll(Merchant[] merchants);
    }
}