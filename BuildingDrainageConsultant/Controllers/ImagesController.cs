namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Models.Images;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class ImagesController : Controller
    {
        private readonly IImageHLService images;
        public ImagesController(IImageHLService images, IMapper mapper)
        {
            this.images = images;
        }

        [HttpPost]
        public IActionResult AddImages(ImageHLViewModel model)
        {
            var galleryName = this.images.AddImagesToGallery(model);

            if (!ModelState.IsValid || model.UploadImageFiles == null)
            {
                return RedirectToAction(galleryName);
            }

            return RedirectToAction(galleryName);
        }

        public IActionResult DrainsGallery(ImageHLViewModel model)
        {
            var drainImages = this.images.GetDrainImages(model.SearchName);

            model.DisplayImages = drainImages;
            return View(model);
        }

        public IActionResult SafeDrainsGallery(ImageHLViewModel model)
        {
            var safeDrainImages = this.images.GetSafeDrainImages(model.SearchName);

            model.DisplayImages = safeDrainImages;
            return View(model);
        }

        public IActionResult AtticaDetailsGallery(ImageHLViewModel model)
        {
            var atticaDetailsImages = this.images.GetAtticaDetailsImages(model.SearchName);

            model.DisplayImages = atticaDetailsImages;
            return View(model);
        }

        public IActionResult AtticaPartsGallery(ImageHLViewModel model)
        {
            var atticaPartsImages = this.images.GetAtticaPartsImages(model.SearchName);

            model.DisplayImages = atticaPartsImages;
            return View(model);
        }

        public IActionResult ArticlesGallery(ImageHLViewModel model)
        {
            var articlesImages = this.images.GetArticlesImages(model.SearchName);

            model.DisplayImages = articlesImages;
            return View(model);
        }

        public IActionResult WaterproofingKitsGallery(ImageHLViewModel model)
        {
            var waterproofingKitsImages = this.images.GetWaterproofingKitsImages(model.SearchName);

            model.DisplayImages = waterproofingKitsImages;
            return View(model);
        }

        public IActionResult AccessoriesGallery(ImageHLViewModel model)
        {
            var accesoriesImages = this.images.GetAccessoriesImages(model.SearchName);

            model.DisplayImages = accesoriesImages;
            return View(model);
        }
        public IActionResult ExtensionsGallery(ImageHLViewModel model)
        {
            var extensionsImages = this.images.GetExtensionsImages(model.SearchName);

            model.DisplayImages = extensionsImages;
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var imageGallery = this.images.GetImageGallery(id);

            if (imageGallery == string.Empty)
            {
                return NotFound();
            }

            var image = this.images.Delete(id);

            return RedirectToAction(imageGallery);
        }
    }
}
