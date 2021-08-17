namespace BuildingDrainageConsultant.Test.Routing
{
    using Xunit;
    using BuildingDrainageConsultant.Controllers;
    using MyTested.AspNetCore.Mvc;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    public class AtticaDrainsControllerTest
    {
        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaDrains/Add")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator"))
                )
                .To<AtticaDrainsController>(c => c.Add(With.Any<AtticaDrainPartsDetailsModel>(), With.Any<int>()));

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaDrains/Add")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDrainsController>(c => c.Add(With.Any<AtticaDrainPartsDetailsModel>(), With.Any<int>()));

        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaDrains/All")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDrainsController>(c => c.All(With.Any<AllAtticaDrainsQueryModel>()));

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/AtticaDrains/Edit/1")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDrainsController>(c => c.Edit(1));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaDrains/Edit/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDrainsController>(c => c.Edit(1, With.Any<AtticaDrainPartsDetailsModel>()));

        [Fact]
        public void PostDeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AtticaDrains/Delete/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<AtticaDrainsController>(c => c.Delete(1));
    }
}
