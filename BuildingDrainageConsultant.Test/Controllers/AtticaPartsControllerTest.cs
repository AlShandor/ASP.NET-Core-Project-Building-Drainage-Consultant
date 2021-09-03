namespace BuildingDrainageConsultant.Test.Controllers
{
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.AtticaParts;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    using static Data.AtticaParts;
    public class AtticaPartsControllerTest
    {

        [Fact]
        public void AddShouldBeForAuthorizedUsersAndReturnView()
        =>
            MyController<AtticaPartsController>
                .Instance()
                .Calling(c => c.Add(new AtticaPartFormModel()))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData(
            "AtticaPart",
            1,
            "Description",1)]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string name,
            int imageId,
            string description,
            int id)
            => MyController<AtticaPartsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .Calling(c => c.Add(new AtticaPartFormModel
                {
                    Name = name,
                    ImageId = imageId,
                    Description = description
                },id))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<AtticaPart>(parts => parts
                        .Any(d =>
                            d.Name == name &&
                            d.ImageId == imageId &&
                            d.Description == description)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<AtticaPartsController>(c => c.All(With.Any<AllAtticaPartsQueryModel>())));

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
            => MyController<AtticaPartsController>
                .Instance(controller => controller
                    .WithData(TenAtticaParts))
                .Calling(c => c.All(new AllAtticaPartsQueryModel()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AllAtticaPartsQueryModel>());

        [Theory]
        [InlineData(1)]
        public void EditShouldBeForAuthorizedUsersAndReturnCorrectViewWithValidModel(int atticaPartilId)
            => MyController<AtticaPartsController>
                .Instance(controller => controller
                    .WithData(new AtticaPart { Id = atticaPartilId }))
                .Calling(c => c.Edit(atticaPartilId))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<AtticaPart>(part => part
                        .Any(d => d.Id == atticaPartilId)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<AtticaPartFormModel>());

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<AtticaPartsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(ValidAtticaPart(1)))
                .Calling(c => c.Edit(1, With.Default<AtticaPartFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<AtticaPartFormModel>());

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<AtticaPartsController>
                .Calling(c => c.Edit(
                    With.Empty<int>(),
                    With.Empty<AtticaPartFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(1)]
        public void DeletePostShouldReturnViewWithCorrectModelWhenCorrectDrainId(int partId)
            => MyController<AtticaPartsController>
                .Instance(instance => instance
                    .WithData(ValidAtticaPart(partId)))
                .Calling(c => c.Delete(partId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<AtticaPartsController>(c => c.All(With.Any<AllAtticaPartsQueryModel>())));

        [Fact]
        public void DeletePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<AtticaPartsController>
                .Calling(c => c.Delete(
                    With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}
