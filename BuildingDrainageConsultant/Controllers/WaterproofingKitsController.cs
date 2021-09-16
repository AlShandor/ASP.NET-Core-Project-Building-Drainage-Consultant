namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.WaterproofingKits;
    using BuildingDrainageConsultant.Services.Images;
    using BuildingDrainageConsultant.Services.WaterproofingKits;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class WaterproofingKitsController : Controller
    {
        private readonly IWaterproofingKitService kits;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public WaterproofingKitsController(
            IWaterproofingKitService kits,
            IImageHLService images,
            IMapper mapper)
        {
            this.kits = kits;
            this.images = images;
            this.mapper = mapper;
        }

        public IActionResult Add(WaterproofingKitFormModel kit) => View(kit);

        [HttpPost]
        public IActionResult Add(WaterproofingKitFormModel kit, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(kit);
            }

            this.kits.Create(
                kit.Name,
                kit.ImageId,
                kit.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllWaterproofingKitsQueryModel query)
        {
            var queryResults = this.kits.All(query.SearchTerm);

            query.WaterproofingKits = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var kit = this.kits.Details(id);

            var waterproofingKitFormModel = this.mapper.Map<WaterproofingKitFormModel>(kit);

            return View(waterproofingKitFormModel);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, WaterproofingKitFormModel waterproofingKit)
        {

            if (!ModelState.IsValid)
            {
                return View(waterproofingKit);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.kits.Edit(
                id,
                waterproofingKit.Name,
                waterproofingKit.ImageId,
                waterproofingKit.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var waterproofingKit = this.kits.Details(id);

            var waterproofingKitForm = this.mapper.Map<WaterproofingKitFormModel>(waterproofingKit);

            return View(waterproofingKitForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var kit = this.kits.Delete(id);

            if (kit == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(WaterproofingKitFormModel kitCreateInfo)
        {
            kitCreateInfo.Images = this.images.GetWaterproofingKitsImages(string.Empty);

            return View(kitCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, WaterproofingKitFormModel kitCreateInfo)
        {
            var waterproofingKitImage = this.images.GetImageById(id);

            if (waterproofingKitImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", kitCreateInfo) });
            }

            kitCreateInfo.Image = waterproofingKitImage;
            kitCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", kitCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, WaterproofingKitFormModel kitCreateInfo)
        {
            kitCreateInfo.Images = this.images.GetWaterproofingKitsImages(string.Empty);
            kitCreateInfo.Id = modelId;

            return View(kitCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, WaterproofingKitFormModel kitCreateInfo)
        {
            var waterproofingKit = this.kits.Details(modelId);
            kitCreateInfo = this.mapper.Map<WaterproofingKitFormModel>(waterproofingKit);

            var waterproofingImage = this.images.GetImageById(id);
            kitCreateInfo.Image = waterproofingImage;
            kitCreateInfo.ImageId = id;

            if (waterproofingImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", kitCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", kitCreateInfo) });
        }
    }
}
