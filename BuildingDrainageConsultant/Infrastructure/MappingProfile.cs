namespace BuildingDrainageConsultant.Infrastructure
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data.Models;

    using BuildingDrainageConsultant.Models.Accessories;
    using BuildingDrainageConsultant.Models.Articles;
    using BuildingDrainageConsultant.Models.AtticaDetails;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Models.AtticaParts;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Models.Extensions;
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Models.SafeDrains;
    using BuildingDrainageConsultant.Models.WaterproofingKits;

    using BuildingDrainageConsultant.Services.Accessories.Models;
    using BuildingDrainageConsultant.Services.Articles.Models;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains.Models;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using BuildingDrainageConsultant.Services.Extensions.Models;
    using BuildingDrainageConsultant.Services.Images.Models;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using BuildingDrainageConsultant.Services.SafeDrains.Models;
    using BuildingDrainageConsultant.Services.WaterproofingKits.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Drain, DrainServiceModel>();
            this.CreateMap<Drain, DrainListingServiceModel>();
            this.CreateMap<DrainServiceModel, DrainFormModel>();

            this.CreateMap<SafeDrain, SafeDrainServiceModel>();
            this.CreateMap<SafeDrain, SafeDrainListingServiceModel>();
            this.CreateMap<SafeDrainServiceModel, SafeDrainFormModel>();

            this.CreateMap<Merchant, MerchantServiceModel>();
            this.CreateMap<MerchantServiceModel, MerchantFormModel>();

            this.CreateMap<AtticaPart, AtticaPartServiceModel>();
            this.CreateMap<AtticaPartServiceModel, AtticaPartFormModel>();

            this.CreateMap<AtticaDetail, AtticaDetailServiceModel>();
            this.CreateMap<AtticaDetailServiceModel, AtticaDetailFormModel>();

            this.CreateMap<AtticaDrain, AtticaDrainServiceModel>();
            this.CreateMap<AtticaDrain, AtticaDrainListingModel>();
            this.CreateMap<AtticaDrainServiceModel, AtticaDrainFormModel>();

            this.CreateMap<Article, ArticleServiceModel>();
            this.CreateMap<ArticleServiceModel, ArticleFormModel>();

            this.CreateMap<ImageHL, ImageHLServiceModel>();

            this.CreateMap<WaterproofingKit, WaterproofingKitServiceModel>();
            this.CreateMap<WaterproofingKitServiceModel, WaterproofingKitFormModel>();

            this.CreateMap<Accessory, AccessoryServiceModel>();
            this.CreateMap<AccessoryServiceModel, AccessoryFormModel>();

            this.CreateMap<Extension, ExtensionServiceModel>();
            this.CreateMap<ExtensionServiceModel, ExtensionFormModel>();
        }
    }
}