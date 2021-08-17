namespace BuildingDrainageConsultant.Test.Routing
{
    using Xunit;
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Models.AtticaParts;
    using MyTested.AspNetCore.Mvc;

    public class AtticaPartsControllerTest
    {
        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaParts/Add")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator"))
                )
                .To<AtticaPartsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaParts/Add")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaPartsController>(c => c.Add(With.Any<AtticaPartFormModel>()));

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaParts/All")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaPartsController>(c => c.All(With.Any<AllAtticaPartsQueryModel>()));

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaParts/Edit/1")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaPartsController>(c => c.Edit(1));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaParts/Edit/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaPartsController>(c => c.Edit(1, With.Any<AtticaPartFormModel>()));

        [Fact]
        public void PostDeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaParts/Delete/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaPartsController>(c => c.Delete(1));
    }
}
