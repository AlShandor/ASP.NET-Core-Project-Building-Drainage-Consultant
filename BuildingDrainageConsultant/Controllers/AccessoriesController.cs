namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Accessories;
    using BuildingDrainageConsultant.Services.Accessories;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;
    public class AccessoriesController : Controller
    {
        private readonly IAccessoryService accessories;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public AccessoriesController(
            IAccessoryService accessories,
            IImageHLService images,
            IMapper mapper)
        {
            this.accessories = accessories;
            this.images = images;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AccessoryFormModel accessory) => View(accessory);

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AccessoryFormModel accessory, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(accessory);
            }

            this.accessories.Create(
                accessory.Name,
                accessory.ImageId,
                accessory.Description);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult All([FromQuery] AllAccessoriesQueryModel query)
        {
            var queryResults = this.accessories.All(query.SearchTerm);

            query.Accessories = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var accessory = this.accessories.Details(id);

            var accessoryForm = this.mapper.Map<AccessoryFormModel>(accessory);

            return View(accessoryForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, AccessoryFormModel accessory)
        {

            if (!ModelState.IsValid)
            {
                return View(accessory);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.accessories.Edit(
                id,
                accessory.Name,
                accessory.ImageId,
                accessory.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var accessory = this.accessories.Details(id);

            var accessoryForm = this.mapper.Map<AccessoryFormModel>(accessory);

            return View(accessoryForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var accessory = this.accessories.Delete(id);

            if (accessory == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(AccessoryFormModel accessoryCreateInfo)
        {
            accessoryCreateInfo.Images = this.images.GetAccessoriesImages(string.Empty);

            return View(accessoryCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, AccessoryFormModel accessoryCreateInfo)
        {
            var accessoryImage = this.images.GetImageById(id);

            if (accessoryImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", accessoryCreateInfo) });
            }

            accessoryCreateInfo.Image = accessoryImage;
            accessoryCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", accessoryCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, AccessoryFormModel accessoryCreateInfo)
        {
            accessoryCreateInfo.Images = this.images.GetAccessoriesImages(string.Empty);
            accessoryCreateInfo.Id = modelId;

            return View(accessoryCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, AccessoryFormModel accessoryCreateInfo)
        {
            var accessory = this.accessories.Details(modelId);
            accessoryCreateInfo = this.mapper.Map<AccessoryFormModel>(accessory);

            var accessoryImage = this.images.GetImageById(id);
            accessoryCreateInfo.Image = accessoryImage;
            accessoryCreateInfo.ImageId = id;

            if (accessoryImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", accessoryCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", accessoryCreateInfo) });
        }
    }
}
