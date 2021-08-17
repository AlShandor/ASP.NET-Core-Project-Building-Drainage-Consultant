namespace BuildingDrainageConsultant.Test.Routing
{
    using Xunit;
    using BuildingDrainageConsultant.Controllers;
    using MyTested.AspNetCore.Mvc;
    using BuildingDrainageConsultant.Models.Drains;

    public class DrainControllerTest
    {
        [Fact]
        public void AddRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Drains/Add")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator"))
                )
                .To<DrainsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Drains/Add")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<DrainsController>(c => c.Add(With.Any<DrainFormModel>()));


        [Fact]
        public void AllRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Drains/All")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<DrainsController>(c => c.All(With.Any<AllDrainsQueryModel>()));

        [Fact]
        public void DetailsRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Drains/Details/1"))
                .To<DrainsController>(c => c.Details(1));

        [Fact]
        public void EditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Drains/Edit/1")
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<DrainsController>(c => c.Edit(1));

        [Fact]
        public void PostEditRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Drains/Edit/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<DrainsController>(c => c.Edit(1, With.Any<DrainFormModel>()));

        [Fact]
        public void PostDeleteRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Drains/Delete/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser(user => user
                        .WithClaim("AdministratorRoleName", "Administrator")))
                .To<DrainsController>(c => c.Delete(1));

        [Fact]
        public void MineRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Drains/Mine/")
                    .WithUser())
                .To<DrainsController>(c => c.Mine());

        [Fact]
        public void AddToMineRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Drains/AddToMine/1")
                    .WithUser())
                .To<DrainsController>(c => c.AddToMine(1));
    }
}
