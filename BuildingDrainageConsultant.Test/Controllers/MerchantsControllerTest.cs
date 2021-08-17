namespace BuildingDrainageConsultant.Test.Controllers
{
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using static Data.Merchants;
    public class MerchantsControllerTest
    {
        [Fact]
        public void AddShouldBeForAuthorizedUsersAndReturnView()
        =>
            MyController<MerchantsController>
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
            "MerchantName",
            "Sofia",
            "Address",
            "asd@abv.bg",
            42.454546,
            23.453463,
            "+35988888",
            "www.example.com")]
        public void PostAddShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string name,
            string city,
            string address,
            string email,
            double latitude,
            double longitude,
            string phone,
            string website
            )
            => MyController<MerchantsController>
                .Instance(controller => controller
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .Calling(c => c.Add(new MerchantFormModel
                {
                    Name = name,
                    City = city,
                    Address = address,
                    Email = email,
                    Latitude = latitude,
                    Longitude = longitude,
                    Phone = phone,
                    Website = website
                }))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Merchant>(parts => parts
                        .Any(d =>
                            d.Name == name &&
                            d.City == city &&
                            d.Address == address &&
                            d.Email == email &&
                            d.Latitude == latitude &&
                            d.Longitude == longitude &&
                            d.Phone == phone &&
                            d.Website == website)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MerchantsController>(c => c.All()));

        [Fact]
        public void AllShouldReturnCorrectViewWithModel()
            => MyController<MerchantsController>
                .Instance(controller => controller
                    .WithData(TenMerchants))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<MerchantServiceModel>>());

        [Theory]
        [InlineData(1)]
        public void EditShouldBeForAuthorizedUsersAndReturnCorrectViewWithValidModel(int merchantId)
            => MyController<MerchantsController>
                .Instance(controller => controller
                    .WithData(new Merchant { Id = merchantId }))
                .Calling(c => c.Edit(merchantId))
                .ShouldHave()
                .ActionAttributes(att => att
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Merchant>(merchant => merchant
                        .Any(d => d.Id == merchantId)))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<MerchantFormModel>());

        [Fact]
        public void EditPostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<MerchantsController>
                .Instance(instance => instance
                    .WithUser()
                    .WithData(ValidMerchant(1)))
                .Calling(c => c.Edit(1, With.Default<MerchantFormModel>()))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<MerchantFormModel>());

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<MerchantsController>
                .Calling(c => c.Edit(
                    With.Empty<int>(),
                    With.Empty<MerchantFormModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Theory]
        [InlineData(1)]
        public void DeletePostShouldReturnViewWithCorrectModelWhenCorrectDrainId(int merchantId)
            => MyController<MerchantsController>
                .Instance(instance => instance
                    .WithData(ValidMerchant(merchantId)))
                .Calling(c => c.Delete(merchantId))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<MerchantsController>(c => c.All()));

        [Fact]
        public void DeletePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<MerchantsController>
                .Calling(c => c.Delete(
                    With.Empty<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

    }
}
