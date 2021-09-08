namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;
    public class DrainsController : Controller
    {
        private readonly IDrainService drains;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public DrainsController(
            IDrainService drains,
            IImageHLService images,
            IMapper mapper)
        {
            this.drains = drains;
            this.images = images;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(DrainFormModel drain, int id) => View(drain);

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(DrainFormModel drain)
        {
            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            var drainId = this.drains.Create(
                  drain.Name,
                  drain.FlowRate,
                  drain.DrainageArea,
                  drain.Depth,
                  drain.Direction,
                  drain.Diameter,
                  drain.VisiblePart,
                  drain.Waterproofing,
                  drain.Heating,
                  drain.Renovation,
                  drain.FlapSeal,
                  drain.ImageId,
                  drain.Description);

            return RedirectToAction(nameof(Edit), new { id = drainId });
        }

        public IActionResult All([FromQuery] AllDrainsQueryModel query)
        {
            var queryResult = this.drains.All(
                query.SearchTerm,
                query.Direction,
                query.Diameter,
                query.VisiblePart,
                query.Waterproofing,
                query.Heating,
                query.Renovation,
                query.FlapSeal,
                query.Sorting,
                query.CurrentPage,
                AllDrainsQueryModel.DrainsPerPage);

            query.TotalDrains = queryResult.TotalDrains;
            query.Drains = queryResult.Drains;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var drain = this.drains.Details(id);

            if (drain == null)
            {
                return NotFound();
            }

            var drainForm = this.mapper.Map<DrainFormModel>(drain);

            if (User.Identity.IsAuthenticated)
            {
                var isMyDrain = this.drains.IsMyDrain(id, this.User.Id());
                drainForm.IsMyDrain = isMyDrain;
            }

            return View(drainForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var drain = this.drains.Details(id);

            var drainForm = this.mapper.Map<DrainFormModel>(drain);

            return View(drainForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, DrainFormModel drain)
        {

            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.drains.Edit(
                id,
                drain.Name,
                drain.FlowRate,
                drain.DrainageArea,
                drain.Depth,
                drain.Direction,
                drain.Diameter,
                drain.VisiblePart,
                drain.Waterproofing,
                drain.Heating,
                drain.Renovation,
                drain.FlapSeal,
                drain.ImageId,
                drain.Description);

            return RedirectToAction(nameof(All));
        }


        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var drain = this.drains.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myDrains = this.drains.ByUser(this.User.Id());

            return View(myDrains);
        }

        [Authorize]
        public IActionResult AddToMine(int id)
        {
            var drain = this.drains.AddToMine(this.User.Id(), id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public IActionResult RemoveFromMine(int id)
        {
            var userId = this.User.Id();

            var drain = this.drains.RemoveFromMine(userId, id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Mine));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(DrainFormModel drainCreateInfo)
        {
            drainCreateInfo.Images = this.images.GetDrainImages();

            return View(drainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, DrainFormModel drainCreateInfo)
        {
            var drainImage = this.images.GetImageById(id);

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", drainCreateInfo) });
            }

            drainCreateInfo.Image = drainImage;
            drainCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", drainCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, DrainFormModel drainCreateInfo)
        {
            drainCreateInfo.Images = this.images.GetDrainImages();
            drainCreateInfo.Id = modelId;

            return View(drainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, DrainFormModel drainCreateInfo)
        {
            var drain = this.drains.Details(modelId);
            drainCreateInfo = this.mapper.Map<DrainFormModel>(drain);

            var drainImage = this.images.GetImageById(id);
            drainCreateInfo.Image = drainImage;
            drainCreateInfo.ImageId = id;

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", drainCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", drainCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddWaterproofingKit(int drainId, DrainFormModel drainCreateInfo)
        {
            drainCreateInfo.WaterproofingKits = this.drains.GetWaterproofingKits();
            drainCreateInfo.Id = drainId;

            return View(drainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddWaterproofingKit(DrainFormModel drainCreateInfo)
        {
            var isAdded = this.drains.AddWaterproofingKit(drainCreateInfo.WaterproofingKitId, drainCreateInfo.Id);

            if (!isAdded)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddWaterproofingKit", drainCreateInfo) });
            }

            var drain = this.drains.Details(drainCreateInfo.Id);
            drainCreateInfo = this.mapper.Map<DrainFormModel>(drain);

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", drainCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult RemoveWaterproofingKit(int kitId, int drainId)
        {
            var waterproofingKit = this.drains.RemoveWaterproofingKit(kitId, drainId);

            if (waterproofingKit == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), new { id = drainId });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddAccessories(int drainId, DrainFormModel drainCreateInfo)
        {
            drainCreateInfo.Accessories = this.drains.GetAccessories();
            drainCreateInfo.Id = drainId;

            return View(drainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddAccessories(DrainFormModel drainCreateInfo)
        {
            var isAdded = this.drains.AddAccessory(drainCreateInfo.AccessoryId, drainCreateInfo.Id);

            if (!isAdded)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddAccessories", drainCreateInfo) });
            }

            var drain = this.drains.Details(drainCreateInfo.Id);
            drainCreateInfo = this.mapper.Map<DrainFormModel>(drain);

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", drainCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult RemoveAccessory(int accessoryId, int drainId)
        {
            var accessory = this.drains.RemoveAccessory(accessoryId, drainId);

            if (accessory == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), new { id = drainId });
        }
    }
}