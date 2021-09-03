namespace BuildingDrainageConsultant.Services.AtticaParts.Models
{
    using BuildingDrainageConsultant.Data.Models;

    public class AtticaPartServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ImageId { get; set; }

        public ImageHL Image { get; set; }
    }
}