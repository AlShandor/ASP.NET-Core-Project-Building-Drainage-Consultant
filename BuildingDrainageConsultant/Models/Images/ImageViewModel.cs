namespace BuildingDrainageConsultant.Models.Images
{
    using BuildingDrainageConsultant.Data.Models;
    using System.Collections.Generic;

    public class ImageViewModel
    {
        public ImageProduct Image { get; set; }
        public List<ImageProduct> Images { get; set; }
    }
}
