namespace BuildingDrainageConsultant.Test.Routing
{
    using Xunit;
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Models.Merchants;
    using MyTested.AspNetCore.Mvc;

    public class MerchantsControllerTest
    {
        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Merchants/Add")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator"))
                )
                .To<MerchantsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Merchants/Add")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<MerchantsController>(c => c.Add(With.Any<MerchantFormModel>()));

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Merchants/All")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<MerchantsController>(c => c.All());

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Merchants/Edit/1")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<MerchantsController>(c => c.Edit(1));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Merchants/Edit/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<MerchantsController>(c => c.Edit(1, With.Any<MerchantFormModel>()));

        [Fact]
        public void PostDeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Merchants/Delete/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<MerchantsController>(c => c.Delete(1));
    }
}
