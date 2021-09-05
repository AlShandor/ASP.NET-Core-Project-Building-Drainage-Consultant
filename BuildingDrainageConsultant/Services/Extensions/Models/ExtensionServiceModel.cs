namespace BuildingDrainageConsultant.Services.Extensions.Models
{
    using BuildingDrainageConsultant.Data.Models;

    public class ExtensionServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}
