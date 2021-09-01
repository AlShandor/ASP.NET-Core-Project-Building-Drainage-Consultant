using BuildingDrainageConsultant.Data.Models;

namespace BuildingDrainageConsultant.Services.Drains.Models
{
    public class DrainDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}