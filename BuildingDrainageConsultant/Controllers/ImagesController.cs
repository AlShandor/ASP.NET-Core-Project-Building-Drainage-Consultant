namespace BuildingDrainageConsultant.Controllers
{
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Images;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class ImagesController : Controller
    {
        BuildingDrainageConsultantDbContext data;
        public ImagesController(BuildingDrainageConsultantDbContext data)
        {
            this.data = data;

        }
        public IActionResult Index()
        {
            ImageViewModel viewModel = new ImageViewModel();
            viewModel.Images = data.Images.ToList();
            viewModel.Image = new ImageProduct();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddImages(ImageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Files = model.Image.ImageFiles;

            if (Files.Count > 0)
            {
                foreach (var img in Files)
                {
                    ImageProduct image = new ImageProduct();
                    var guid = Guid.NewGuid().ToString();
                    var filePath = "wwwroot/images/" + img.FileName;
                    var fileName = img.FileName;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        img.CopyTo(stream);
                        image.Name = fileName;
                        image.Path = filePath;
                        data.Images.Add(image);
                        data.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
        }
    }
}
