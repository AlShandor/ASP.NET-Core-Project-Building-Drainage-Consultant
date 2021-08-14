namespace BuildingDrainageConsultant.Infrastructure
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.AtticaDetails;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Models.AtticaParts;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
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

            this.CreateMap<AtticaPart, AtticaPartServiceModel>();
            this.CreateMap<AtticaPartServiceModel, AtticaPartFormModel>();

            this.CreateMap<AtticaDetail, AtticaDetailServiceModel>();
            this.CreateMap<AtticaDetailServiceModel, AtticaDetailFormModel>();

            this.CreateMap<AtticaDrain, AtticaDrainServiceModel>();
            this.CreateMap<AtticaDrainServiceModel, AtticaDrainFormModel>(); 
            this.CreateMap<AtticaDrainServiceModel, AtticaDrainPartsDetailsModel>();
        }
    }
}