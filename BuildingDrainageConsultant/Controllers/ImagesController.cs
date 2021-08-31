namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Images;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class ImagesController : Controller
    {
        private readonly IImageHLService images;
        private readonly IMapper mapper;
        public ImagesController(IImageHLService images, IMapper mapper)
        {
            this.images = images;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddImages(ImageHLViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var galleryName = this.images.AddImagesToGallery(model);

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
    }
}
