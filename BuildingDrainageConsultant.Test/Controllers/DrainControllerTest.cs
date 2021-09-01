namespace BuildingDrainageConsultant.Test.Controllers
{
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains.Models;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using static Data.Drains;
    public class DrainControllerTest
    {

        [Fact]
        public void AddShouldBeForAuthorizedUsersAndReturnView()
        =>
            MyController<DrainsController>
                .Instance()
                .Calling(c => c.Add(new DrainFormModel()))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();


        [Theory]
        [InlineData(
            "DrainName1",
            3.5,
            5,
            8,
            DrainDirectionEnum.Horizontal,
            DrainDiameterEnum.DN110,
            DrainVisiblePartEnum.Grate,
            DrainWaterproofingEnum.Bitumen,
            DrainHeatingEnum.NoHeating,
            DrainRenovationEnum.ForRenovation,
            DrainFlapSealEnum.NoFlapSeal,
            1,
            "drain1 Description")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string name,
            double flowRate,
            int drainageArea,
            int depth,
            DrainDirectionEnum direction,
            DrainDiameterEnum diameter,
            DrainVisiblePartEnum visiblePart,
            DrainWaterproofingEnum waterproofing,
            DrainHeatingEnum heating,
            DrainRenovationEnum renovation,
            DrainFlapSealEnum flapSeal,
            int imageId,
            string description)
        => MyController<DrainsController>
            .Instance(controller => controller
                .WithUser(user => user
                    .WithClaim("AdministratorRoleName", "Administrator")))
            .Calling(c => c.Add(new DrainFormModel
            {
                Name = name,
                FlowRate = flowRate,
                DrainageArea = drainageArea,
                Depth = depth,
                Direction = direction,
                Diameter = diameter,
                VisiblePart = visiblePart,
                Waterproofing = waterproofing,
                Heating = heating,
                Renovation = renovation,
                FlapSeal = flapSeal,
                ImageId = imageId,
                Description = description
            }))
            .ShouldHave()
            .ActionAttributes(att => att
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Drain>(drains => drains
                    .Any(d =>
                        d.Name == name &&
                        d.FlowRate == flowRate &&
                        d.DrainageArea == drainageArea &&
                        d.Depth == depth &&
                        d.Direction == direction &&
                        d.Diameter == diameter &&
                        d.VisiblePart == visiblePart &&
                        d.Waterproofing == waterproofing &&
                        d.Heating == heating &&
                        d.Renovation == renovation &&
                        d.FlapSeal == flapSeal &&
                        d.ImageId == imageId &&
                        d.Description == description)))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
                .To<DrainsController>(c => c.All(With.Any<AllDrainsQueryModel>())));

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
            => MyController<DrainsController>
                .Instance(controller => controller
                    .WithData(TenDrains))
                .Calling(c => c.All(new AllDrainsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllDrainsQueryModel>());

        [Theory]
        [InlineData(1)]
        public void DetailsShouldReturnCorrectViewWithValidModel(int drainId)
            => MyController<DrainsController>
                .Instance(controller => controller
                    .WithData(new Drain { Id = drainId }))
                .Calling(c => c.Details(drainId))
                .ShouldHave()
                .ValidModelState()
                .Data(data => data
                    .WithSet<Drain>(drains => drains
                        .Any(d => d.Id == drainId)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<DrainFormModel>());

        [Theory]
        [InlineData(1)]
        public void EditShouldBeForAuthorizedUsersAndReturnCorrectViewWithValidModel(int drainId)
            => MyController<DrainsController>
                .Instance(controller => controller
                    .WithData(new Drain { Id = drainId }))
                .Calling(c => c.Edit(drainId))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Drain>(drains => drains
                        .Any(d => d.Id == drainId)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<DrainFormModel>());


        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<DrainsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(ValidDrain(1)))
                .Calling(c => c.Edit(1, With.Default<DrainFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<DrainFormModel>());

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<DrainsController>
                .Calling(c => c.Edit(
                    With.Empty<int>(),
                    With.Empty<DrainFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(1)]
        public void DeletePostShouldReturnViewWithCorrectModelWhenCorrectDrainId(int drainId)
            => MyController<DrainsController>
                .Instance(instance => instance
                    .WithData(ValidDrain(drainId)))
                .Calling(c => c.Delete(drainId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DrainsController>(c => c.All(With.Any<AllDrainsQueryModel>())));

        [Fact]
        public void DeletePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<DrainsController>
                .Calling(c => c.Delete(
                    With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(1)]
        public void AddToMineShouldBeForAuthorizedUsersAndReturnCorrectViewWithValidModel(int drainId)
            => MyController<DrainsController>
                .Instance(controller => controller
                    .WithUser("Author Id 1", "Author 1")
                    .WithData(GetUserDrains(drainId, sameUser: false)))
                .Calling(c => c.AddToMine(drainId))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<User>(users => users
                        .Any(u => u.Drains.Any(d => d.Id == drainId))))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<DrainsController>(c => c.Details(drainId)));

    }
}
