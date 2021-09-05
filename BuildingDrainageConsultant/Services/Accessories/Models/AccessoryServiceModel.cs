namespace BuildingDrainageConsultant.Services.Accessories.Models
{
    using BuildingDrainageConsultant.Data.Models;

    public class AccessoryServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}
