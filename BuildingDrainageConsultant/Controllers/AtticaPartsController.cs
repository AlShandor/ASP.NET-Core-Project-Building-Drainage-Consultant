namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaParts;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    public class AtticaPartsController : Controller
    {
        private readonly IAtticaPartService parts;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public AtticaPartsController(
            IAtticaPartService parts,
            IImageHLService images,
            IMapper mapper)
        {
            this.parts = parts;
            this.images = images;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AtticaPartFormModel atticaPart) => View(atticaPart);

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AtticaPartFormModel atticaPart, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaPart);
            }

            this.parts.Create(
                atticaPart.Name,
                atticaPart.ImageId,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult All([FromQuery] AllAtticaPartsQueryModel query)
        {
            var queryResults = this.parts.All(query.SearchTerm);

            query.AtticaParts = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var atticaPart = this.parts.Details(id);

            var atticaPartForm = this.mapper.Map<AtticaPartFormModel>(atticaPart);

            return View(atticaPartForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, AtticaPartFormModel atticaPart)
        {

            if (!ModelState.IsValid)
            {
                return View(atticaPart);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.parts.Edit(
                id,
                atticaPart.Name,
                atticaPart.ImageId,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var atticaPart = this.parts.Details(id);

            var atticaPartForm = this.mapper.Map<AtticaPartFormModel>(atticaPart);

            return View(atticaPartForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var drain = this.parts.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(AtticaPartFormModel partCreateInfo)
        {
            partCreateInfo.Images = this.images.GetAtticaPartsImages(string.Empty);

            return View(partCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, AtticaPartFormModel partCreateInfo)
        {
            var atticaPartImage = this.images.GetImageById(id);

            if (atticaPartImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", partCreateInfo) });
            }

            partCreateInfo.Image = atticaPartImage;
            partCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", partCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, AtticaPartFormModel partCreateInfo)
        {
            partCreateInfo.Images = this.images.GetAtticaPartsImages(string.Empty);
            partCreateInfo.Id = modelId;

            return View(partCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, AtticaPartFormModel partCreateInfo)
        {
            var atticaPart = this.parts.Details(modelId);
            partCreateInfo = this.mapper.Map<AtticaPartFormModel>(atticaPart);

            var drainImage = this.images.GetImageById(id);
            partCreateInfo.Image = drainImage;
            partCreateInfo.ImageId = id;

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", partCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", partCreateInfo) });
        }
    }
}