namespace BuildingDrainageConsultant.Test.Routing
{
    using Xunit;
    using BuildingDrainageConsultant.Controllers;
    using MyTested.AspNetCore.Mvc;
    using BuildingDrainageConsultant.Models.AtticaDetails;

    public class AtticaDetailsControllerTest
    {
        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaDetails/Add")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator"))
                )
                .To<AtticaDetailsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaDetails/Add")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDetailsController>(c => c.Add(With.Any<AtticaDetailFormModel>()));

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaDetails/All")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDetailsController>(c => c.All(With.Any<AllAtticaDetailsQueryModel>()));

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaDetails/Edit/1")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDetailsController>(c => c.Edit(1));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaDetails/Edit/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDetailsController>(c => c.Edit(1, With.Any<AtticaDetailFormModel>()));

        [Fact]
        public void PostDeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaDetails/Delete/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDetailsController>(c => c.Delete(1));
    }
}
