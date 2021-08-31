namespace BuildingDrainageConsultant.Services.Images
{
    using BuildingDrainageConsultant.Models.Images;
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;

    public interface IImageHLService
    {
        public string AddImagesToGallery(ImageHLViewModel model);

        public IEnumerable<ImageHLServiceModel> GetDrainImages();

        public IEnumerable<ImageHLServiceModel> GetAtticaDetailsImages();

        public IEnumerable<ImageHLServiceModel> GetAtticaPartsImages();

        public IEnumerable<ImageHLServiceModel> GetArticlesImages();

        public bool Delete(int id);

        public string GetImageGallery(int id);
    }
}
