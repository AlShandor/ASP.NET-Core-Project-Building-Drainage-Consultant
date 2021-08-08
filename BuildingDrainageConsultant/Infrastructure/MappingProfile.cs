namespace BuildingDrainageConsultant.Infrastructure
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using BuildingDrainageConsultant.Services.Merchants.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Drain, DrainServiceModel>();
            this.CreateMap<Drain, DrainDetailsServiceModel>();
            this.CreateMap<DrainServiceModel, DrainFormModel>();

            this.CreateMap<Merchant, MerchantServiceModel>();
            this.CreateMap<MerchantServiceModel, MerchantFormModel>();
        }
    }
}
