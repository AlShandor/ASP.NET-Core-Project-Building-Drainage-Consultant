namespace BuildingDrainageConsultant.Services.SafeDrains.Models
{
    using BuildingDrainageConsultant.Data.Models;

    public class SafeDrainListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}
