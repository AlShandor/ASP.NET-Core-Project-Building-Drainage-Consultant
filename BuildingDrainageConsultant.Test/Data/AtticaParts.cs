namespace BuildingDrainageConsultant.Test.Data
{
    using BuildingDrainageConsultant.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AtticaParts
    {
        public static IEnumerable<AtticaPart> TenAtticaParts
            => Enumerable.Range(0, 10).Select(i => new AtticaPart());

        public static IEnumerable<AtticaPart> ValidAtticaPart(int partId)
            => Enumerable.Range(1, 1).Select(i => new AtticaPart
            {
                Name = "ValidAtticaDrain",
                ImageId = 1,
                Description = "Description"
            });
    }
}
