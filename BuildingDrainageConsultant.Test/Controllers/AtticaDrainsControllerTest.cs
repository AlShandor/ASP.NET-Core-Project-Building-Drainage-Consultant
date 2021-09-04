namespace BuildingDrainageConsultant.Test.Controllers
{
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    using static Data.AtticaDrains;
    public class AtticaDrainsControllerTest
    {
        [Fact]
        public void AddShouldBeForAuthorizedUsersAndReturnView()
        =>
            MyController<AtticaDrainsController>
                .Instance()
                .Calling(c => c.Add(With.Empty<AtticaDrainFormModel>(),
                                    With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(
            1,
            "AtticaDrainName1",
            3.5,
            5,
            AtticaScreedWaterproofingEnum.Bitumen,
            AtticaConcreteWaterproofingEnum.Bitumen,
            AtticaDiameterEnum.DN110,
            AtticaVisiblePartEnum.DomedLeafCatcherWarmRoof)]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            int atticaDetailId,
            string name,
            double flowRate,
            int drainageArea,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            AtticaConcreteWaterproofingEnum concreteWaterproofing,
            AtticaDiameterEnum diameter,
            AtticaVisiblePartEnum visiblePart)
        => MyController<AtticaDrainsController>
            .Instance(controller => controller
                .WithData(new AtticaDetail
                {
                    Id = atticaDetailId,
                    RoofType = AtticaRoofTypeEnum.ColdRoof,
                    IsWalkable = AtticaWalkableEnum.NotWalkable,
                    Description = "Description"
                })
                .WithUser(user => user
                    .WithClaim("AdministratorRoleName", "Administrator")))
            .Calling(c => c.Add(new AtticaDrainFormModel
            {
                AtticaDetailId = atticaDetailId,
                Name = name,
                FlowRate = flowRate,
                DrainageArea = drainageArea,
                ScreedWaterproofing = screedWaterproofing,
                ConcreteWaterproofing = concreteWaterproofing,
                Diameter = diameter,
                VisiblePart = visiblePart
            }))
            .ShouldHave()
            .ActionAttributes(att => att
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<AtticaDrain>(drains => drains
                    .Any(d =>
                        d.AtticaDetailId == atticaDetailId &&
                        d.Name == name &&
                        d.FlowRate == flowRate &&
                        d.DrainageArea == drainageArea &&
                        d.ScreedWaterproofing == screedWaterproofing &&
                        d.ConcreteWaterproofing == concreteWaterproofing &&
                        d.Diameter == diameter &&
                        d.VisiblePart == visiblePart)))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
                .To<AtticaDrainsController>(c => c.Edit(atticaDetailId)));

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
            => MyController<AtticaDrainsController>
                .Instance(controller => controller
                    .WithData(TenAtticaDrains))
                .Calling(c => c.All(new AllAtticaDrainsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllAtticaDrainsQueryModel>());

        [Theory]
        [InlineData(1)]
        public void EditShouldBeForAuthorizedUsersAndReturnCorrectViewWithValidModel(int atticaDrainId)
            => MyController<AtticaDrainsController>
                .Instance(controller => controller
                    .WithData(new AtticaDrain { Id = atticaDrainId }))
                .Calling(c => c.Edit(atticaDrainId))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<AtticaDrain>(drains => drains
                        .Any(d => d.Id == atticaDrainId)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AtticaDrainFormModel>());

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<AtticaDrainsController>
                .Calling(c => c.Edit(
                    With.Empty<int>(),
                    With.Empty<AtticaDrainFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(1)]
        public void DeletePostShouldReturnViewWithCorrectModelWhenCorrectDrainId(int atticaDrainId)
            => MyController<AtticaDrainsController>
                .Instance(instance => instance
                    .WithData(ValidAtticaDrain(atticaDrainId)))
                .Calling(c => c.Delete(atticaDrainId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<AtticaDrainsController>(c => c.All(With.Any<AllAtticaDrainsQueryModel>())));

        [Fact]
        public void DeletePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<AtticaDrainsController>
                .Calling(c => c.Delete(
                    With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}
