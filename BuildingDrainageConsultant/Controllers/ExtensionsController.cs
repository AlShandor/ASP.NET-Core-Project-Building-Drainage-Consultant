namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Extensions;
    using BuildingDrainageConsultant.Services.Extensions;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    public class ExtensionsController : Controller
    {
        private readonly IExtensionService extensions;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public ExtensionsController(
            IExtensionService extensions,
            IImageHLService images,
            IMapper mapper)
        {
            this.extensions = extensions;
            this.images = images;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(ExtensionFormModel extension) => View(extension);

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(ExtensionFormModel extension, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(extension);
            }

            this.extensions.Create(
                extension.Name,
                extension.ImageId,
                extension.Description);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult All([FromQuery] AllExtensionsQueryModel query)
        {
            var queryResults = this.extensions.All(query.SearchTerm);

            query.Extensions = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var extension = this.extensions.Details(id);

            var extensionForm = this.mapper.Map<ExtensionFormModel>(extension);

            return View(extensionForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, ExtensionFormModel extension)
        {

            if (!ModelState.IsValid)
            {
                return View(extension);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.extensions.Edit(
                id,
                extension.Name,
                extension.ImageId,
                extension.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var extension = this.extensions.Details(id);

            var extensionForm = this.mapper.Map<ExtensionFormModel>(extension);

            return View(extensionForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var extension = this.extensions.Delete(id);

            if (extension == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(ExtensionFormModel extensionCreateInfo)
        {
            extensionCreateInfo.Images = this.images.GetExtensionsImages(string.Empty);

            return View(extensionCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, ExtensionFormModel extensionCreateInfo)
        {
            var extensionImage = this.images.GetImageById(id);

            if (extensionImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", extensionCreateInfo) });
            }

            extensionCreateInfo.Image = extensionImage;
            extensionCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", extensionCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, ExtensionFormModel extensionCreateInfo)
        {
            extensionCreateInfo.Images = this.images.GetExtensionsImages(string.Empty);
            extensionCreateInfo.Id = modelId;

            return View(extensionCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, ExtensionFormModel extensionCreateInfo)
        {
            var extension = this.extensions.Details(modelId);
            extensionCreateInfo = this.mapper.Map<ExtensionFormModel>(extension);

            var extensionImage = this.images.GetImageById(id);
            extensionCreateInfo.Image = extensionImage;
            extensionCreateInfo.ImageId = id;

            if (extensionImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", extensionCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", extensionCreateInfo) });
        }
    }
}
