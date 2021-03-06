namespace BuildingDrainageConsultant.Services.Images
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Data.Models.Enums.ImagesHL;
    using BuildingDrainageConsultant.Models.Images;
    using BuildingDrainageConsultant.Services.Images.Models;
    using System.Collections.Generic;

    public interface IImageHLService
    {
        public string AddImagesToGallery(ImageHLViewModel model);

        public ImageHLServiceModel GetImageById(int id);

        public IEnumerable<ImageHLServiceModel> GetDrainImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetSafeDrainImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetAtticaDetailsImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetAtticaPartsImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetArticlesImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetWaterproofingKitsImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetAccessoriesImages(string searchName);

        public IEnumerable<ImageHLServiceModel> GetExtensionsImages(string searchName);

        public ImageHL GetImageIdByNameAndCategory(string name, ImageHLCategoriesEnum category);

        public bool Delete(int id);

        public string GetImageGallery(int id);
    }
}
