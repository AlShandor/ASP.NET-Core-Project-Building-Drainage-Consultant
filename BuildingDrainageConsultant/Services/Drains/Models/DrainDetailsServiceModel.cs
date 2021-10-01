using BuildingDrainageConsultant.Data.Models;
using BuildingDrainageConsultant.Services.WaterproofingKits.Models;
using System.Collections.Generic;

namespace BuildingDrainageConsultant.Services.Drains.Models
{
    public class DrainDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }

        public int? WaterproofingKitId { get; set; }

        public WaterproofingKitServiceModel WaterproofingKit { get; set; }

        public ICollection<Accessory> Accessories { get; set; }
    }
}