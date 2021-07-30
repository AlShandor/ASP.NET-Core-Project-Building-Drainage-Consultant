namespace BuildingDrainageConsultant.Infrastructure
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Drain, DrainServiceModel>();
            this.CreateMap<Drain, DrainDetailsServiceModel>();

            this.CreateMap<DrainServiceModel, DrainFormModel>();
        }
    }
}
