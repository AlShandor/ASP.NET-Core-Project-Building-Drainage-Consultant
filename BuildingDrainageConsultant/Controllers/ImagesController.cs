﻿namespace BuildingDrainageConsultant.Controllers
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
            var drainImages = this.images.GetDrainImages();

            model.DisplayImages = drainImages;
            return View(model);
        }

        public IActionResult AtticaDetailsGallery(ImageHLViewModel model)
        {
            var drainImages = this.images.GetAtticaDetailsImages();

            model.DisplayImages = drainImages;
            return View(model);
        }

        public IActionResult AtticaPartsGallery(ImageHLViewModel model)
        {
            var drainImages = this.images.GetAtticaPartsImages();

            model.DisplayImages = drainImages;
            return View(model);
        }

        public IActionResult ArticlesGallery(ImageHLViewModel model)
        {
            var drainImages = this.images.GetArticlesImages();

            model.DisplayImages = drainImages;
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
