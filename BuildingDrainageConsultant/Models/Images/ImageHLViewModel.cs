namespace BuildingDrainageConsultant.Models.Images
{
    using BuildingDrainageConsultant.Services.Images.Models;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class ImageHLViewModel
    {
        public string ImageCategory { get; set; }

        public IEnumerable<ImageHLServiceModel> DisplayImages { get; set; }

        public ICollection<IFormFile> UploadImageFiles { get; set; }
    }
}
