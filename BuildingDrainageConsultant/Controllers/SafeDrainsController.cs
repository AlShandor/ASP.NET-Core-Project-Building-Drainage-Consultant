namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Services.Images;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.SafeDrains;
    using BuildingDrainageConsultant.Services.SafeDrains;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;
    public class SafeDrainsController : Controller
    {
        private readonly ISafeDrainService safeDrains;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public SafeDrainsController(
            ISafeDrainService safeDrains,
            IImageHLService images,
            IMapper mapper)
        {
            this.safeDrains = safeDrains;
            this.images = images;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(SafeDrainFormModel drain, int id) => View(drain);

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(SafeDrainFormModel drain)
        {
            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            var drainId = this.safeDrains.Create(
                  drain.Name,
                  drain.FlowRateFree,
                  drain.FlowRate3mVertical,
                  drain.DrainageAreaFree,
                  drain.DrainageArea3mVertical,
                  drain.Depth,
                  drain.Direction,
                  drain.Diameter,
                  drain.Waterproofing,
                  drain.Heating,
                  drain.ImageId,
                  drain.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(AllSafeDrainsQueryModel query)
        {
            var queryResult = this.safeDrains.All(
                query.SearchTerm,
                query.Direction,
                query.Diameter,
                query.Waterproofing,
                query.Heating,
                query.Sorting,
                query.CurrentPage,
                query.DrainsPerPage);

            query.TotalDrains = queryResult.TotalDrains;
            query.Drains = queryResult.Drains;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var safeDrain = this.safeDrains.Details(id);

            if (safeDrain == null)
            {
                return NotFound();
            }

            var safeDrainForm = this.mapper.Map<SafeDrainFormModel>(safeDrain);

            if (User.Identity.IsAuthenticated)
            {
                var isMyDrain = this.safeDrains.IsMyDrain(id, this.User.Id());
                safeDrainForm.IsMyDrain = isMyDrain;
            }

            return View(safeDrainForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var safeDrain = this.safeDrains.Details(id);

            var safeDrainForm = this.mapper.Map<SafeDrainFormModel>(safeDrain);

            return View(safeDrainForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, SafeDrainFormModel drain)
        {

            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.safeDrains.Edit(
                id,
                drain.Name,
                drain.FlowRateFree,
                drain.FlowRate3mVertical,
                drain.DrainageAreaFree,
                drain.DrainageArea3mVertical,
                drain.Depth,
                drain.Direction,
                drain.Diameter,
                drain.Waterproofing,
                drain.Heating,
                drain.ImageId,
                drain.Description);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var safeDrain = this.safeDrains.Delete(id);

            if (safeDrain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var mySafeDrains = this.safeDrains.ByUser(this.User.Id());

            return View(mySafeDrains);
        }

        [Authorize]
        public IActionResult AddToMine(int id)
        {
            var safeDrain = this.safeDrains.AddToMine(this.User.Id(), id);

            if (safeDrain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public IActionResult RemoveFromMine(int id)
        {
            var userId = this.User.Id();

            var safeDrain = this.safeDrains.RemoveFromMine(userId, id);

            if (safeDrain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Mine));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(SafeDrainFormModel safeDrainCreateInfo)
        {
            safeDrainCreateInfo.Images = this.images.GetSafeDrainImages(string.Empty);

            return View(safeDrainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, SafeDrainFormModel safeDrainCreateInfo)
        {
            var safeDrainImage = this.images.GetImageById(id);

            if (safeDrainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", safeDrainCreateInfo) });
            }

            safeDrainCreateInfo.Image = safeDrainImage;
            safeDrainCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", safeDrainCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, SafeDrainFormModel safeDrainCreateInfo)
        {
            safeDrainCreateInfo.Images = this.images.GetSafeDrainImages(string.Empty);
            safeDrainCreateInfo.Id = modelId;

            return View(safeDrainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, SafeDrainFormModel safeDrainCreateInfo)
        {
            var safeDrain = this.safeDrains.Details(modelId);
            safeDrainCreateInfo = this.mapper.Map<SafeDrainFormModel>(safeDrain);

            var drainImage = this.images.GetImageById(id);
            safeDrainCreateInfo.Image = drainImage;
            safeDrainCreateInfo.ImageId = id;

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", safeDrainCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", safeDrainCreateInfo) });
        }
    }
}
