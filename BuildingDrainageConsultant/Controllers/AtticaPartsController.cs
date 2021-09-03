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
        private readonly IAtticaPartService atticaParts;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public AtticaPartsController(
            IAtticaPartService parts,
            IImageHLService images,
            IMapper mapper)
        {
            this.atticaParts = parts;
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

            this.atticaParts.Create(
                atticaPart.Name,
                atticaPart.ImageId,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult All([FromQuery] AllAtticaPartsQueryModel query)
        {
            var queryResults = this.atticaParts.All(query.SearchTerm);

            query.AtticaParts = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var atticaPart = this.atticaParts.Details(id);

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

            this.atticaParts.Edit(
                id,
                atticaPart.Name,
                atticaPart.ImageId,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var atticaPart = this.atticaParts.Details(id);

            var atticaPartForm = this.mapper.Map<AtticaPartFormModel>(atticaPart);

            return View(atticaPartForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var drain = this.atticaParts.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddAtticaPartImage(AtticaPartFormModel partCreateInfo)
        {
            partCreateInfo.Images = this.images.GetAtticaPartsImages();

            return View(partCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddAtticaPartImage(int id, AtticaPartFormModel partCreateInfo)
        {
            var atticaPartImage = this.images.GetImageById(id);

            if (atticaPartImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddAtticaPartImage", partCreateInfo) });
            }

            partCreateInfo.Image = atticaPartImage;
            partCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", partCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditAtticaPartImage(int atticaPartId, AtticaPartFormModel partCreateInfo)
        {
            partCreateInfo.Images = this.images.GetAtticaPartsImages();
            partCreateInfo.Id = atticaPartId;

            return View(partCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditAtticaPartImage(int id, int atticaPartId, AtticaPartFormModel partCreateInfo)
        {
            var atticaPart = this.atticaParts.Details(atticaPartId);
            partCreateInfo = this.mapper.Map<AtticaPartFormModel>(atticaPart);

            var drainImage = this.images.GetImageById(id);
            partCreateInfo.Image = drainImage;
            partCreateInfo.ImageId = id;

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditAtticaPartImage", partCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", partCreateInfo) });
        }
    }
}