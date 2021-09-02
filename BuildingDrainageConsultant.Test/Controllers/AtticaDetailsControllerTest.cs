namespace BuildingDrainageConsultant.Test.Controllers
{
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Attica;
    using BuildingDrainageConsultant.Models.AtticaDetails;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    using static Data.AtticaDetails;
    public class AtticaDetailsControllerTest
    {
        [Fact]
        public void AddShouldBeForAuthorizedUsersAndReturnView()
        =>
            MyController<AtticaDetailsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(
            AtticaRoofTypeEnum.ColdRoof,
            AtticaWalkableEnum.NotWalkable,
            AtticaScreedWaterproofingEnum.Bitumen,
            "AtticaDrain Description",
            1)]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            AtticaRoofTypeEnum roofType,
            AtticaWalkableEnum isWalkable,
            AtticaScreedWaterproofingEnum screedWaterproofing,
            string Description,
            int ImageId
            )
            => MyController<AtticaDetailsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .Calling(c => c.Add(new AtticaDetailFormModel
                {
                    RoofType = roofType,
                    IsWalkable = isWalkable,
                    ScreedWaterproofing = screedWaterproofing,
                    Description = Description,
                    ImageId = ImageId
                }))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<AtticaDetail>(details => details
                        .Any(d =>
                            d.RoofType == roofType &&
                            d.IsWalkable == isWalkable &&
                            d.ScreedWaterproofing == screedWaterproofing &&
                            d.Description == Description &&
                            d.ImageId == ImageId)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<AtticaDetailsController>(c => c.All(With.Any<AllAtticaDetailsQueryModel>())));

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
            => MyController<AtticaDetailsController>
                .Instance(controller => controller
                    .WithData(TenAtticaDetails))
                .Calling(c => c.All(new AllAtticaDetailsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllAtticaDetailsQueryModel>());

        [Theory]
        [InlineData(1)]
        public void EditShouldBeForAuthorizedUsersAndReturnCorrectViewWithValidModel(int atticaDetailId)
            => MyController<AtticaDetailsController>
                .Instance(controller => controller
                    .WithData(new AtticaDetail { Id = atticaDetailId }))
                .Calling(c => c.Edit(atticaDetailId))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<AtticaDetail>(detail => detail
                        .Any(d => d.Id == atticaDetailId)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AtticaDetailFormModel>());

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<AtticaDetailsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(ValidAtticaDetail(1)))
                .Calling(c => c.Edit(1, With.Default<AtticaDetailFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<AtticaDetailFormModel>());

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<AtticaDetailsController>
                .Calling(c => c.Edit(
                    With.Empty<int>(),
                    With.Empty<AtticaDetailFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(1)]
        public void DeletePostShouldReturnViewWithCorrectModelWhenCorrectDrainId(int atticaDetailId)
            => MyController<AtticaDetailsController>
                .Instance(instance => instance
                    .WithData(ValidAtticaDetail(atticaDetailId)))
                .Calling(c => c.Delete(atticaDetailId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<AtticaDetailsController>(c => c.All(With.Any<AllAtticaDetailsQueryModel>())));

        [Fact]
        public void DeletePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<AtticaDetailsController>
                .Calling(c => c.Delete(
                    With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}
