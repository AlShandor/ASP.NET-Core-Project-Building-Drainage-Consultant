namespace BuildingDrainageConsultant.Test.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.Drains;
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public static class Drains
    {
        public static IEnumerable<Drain> TenDrains
            => Enumerable.Range(0, 10).Select(i => new Drain());

        public static IEnumerable<Drain> ValidDrain(int drainId)
            => Enumerable.Range(1, 1).Select(i => new Drain
            {
                Id = drainId,
                Name = "OriginalName",
                FlowRate = 2,
                DrainageArea = 3,
                Depth = 4,
                Direction = DrainDirectionEnum.Horizontal,
                Diameter = DrainDiameterEnum.DN110,
                VisiblePart = DrainVisiblePartEnum.Grate,
                Waterproofing = DrainWaterproofingEnum.Bitumen,
                Heating = DrainHeatingEnum.NoHeating,
                Renovation = DrainRenovationEnum.ForRenovation,
                FlapSeal = DrainFlapSealEnum.NoFlapSeal,
                ImageUrl = "Test ImageUrl",
                Description = "Test Description"
            });


        public static List<Drain> GetUserDrains(int drainId, bool sameUser = true)
        {

            var user = new User
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username
            };

            var userData = sameUser ? user : new User
            {
                Id = $"Author Id {1}",
                UserName = $"Author {1}"
            };

            var listUsers = new List<User>();
            listUsers.Add(userData);

            var drains = Enumerable
                .Range(1, 1)
                .Select(i => new Drain
                {
                    Id = drainId,
                    Name = "OriginalName",
                    FlowRate = 2,
                    DrainageArea = 3,
                    Depth = 4,
                    Direction = DrainDirectionEnum.Horizontal,
                    Diameter = DrainDiameterEnum.DN110,
                    VisiblePart = DrainVisiblePartEnum.Grate,
                    Waterproofing = DrainWaterproofingEnum.Bitumen,
                    Heating = DrainHeatingEnum.NoHeating,
                    Renovation = DrainRenovationEnum.ForRenovation,
                    FlapSeal = DrainFlapSealEnum.NoFlapSeal,
                    ImageUrl = "Test ImageUrl",
                    Description = "Test Description",
                    Users = listUsers
                })
                .ToList();

            return drains;
        }
    }
}
