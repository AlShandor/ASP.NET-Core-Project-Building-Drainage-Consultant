
namespace BuildingDrainageConsultant.Services.Merchants
{
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using System.Collections.Generic;

    public interface IMerchantService
    {
        public IEnumerable<MerchantServiceModel> All(IEnumerable<MerchantServiceModel> merchants);

        public int Create(
            string name, 
            string city, 
            string address,
            string email,
            string phone,
            string website,
            double latitude,
            double longitude);

        public bool Edit(
            int id,
            string name,
            string city,
            string address,
            string email,
            string phone,
            string website,
            double latitude,
            double longitude);

        public MerchantServiceModel Details(int id);

        public bool Delete(int id);
    }
}
