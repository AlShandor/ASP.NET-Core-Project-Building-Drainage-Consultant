namespace BuildingDrainageConsultant.Services.Images
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Images;
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;

    public interface IImageHLService
    {
        public string AddImagesToGallery(ImageHLViewModel model);

        public ImageHLServiceModel GetImageById(int id);

        public IEnumerable<ImageHLServiceModel> GetDrainImages();

        public IEnumerable<ImageHLServiceModel> GetAtticaDetailsImages();

        public IEnumerable<ImageHLServiceModel> GetAtticaPartsImages();

        public IEnumerable<ImageHLServiceModel> GetArticlesImages();

        public IEnumerable<ImageHLServiceModel> GetWaterproofingKitsImages();

        public bool Delete(int id);

        public string GetImageGallery(int id);

        public void CreateAll(ImageHL[] drains);
    }
}
