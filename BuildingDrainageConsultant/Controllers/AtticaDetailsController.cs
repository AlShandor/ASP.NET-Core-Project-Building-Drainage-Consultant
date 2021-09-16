namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDetails;
    using BuildingDrainageConsultant.Services.AtticaDetail;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;
    public class AtticaDetailsController : Controller
    {
        private readonly IAtticaDetailService atticaDetails;
        private readonly IImageHLService images;
        private readonly IMapper mapper;
        public AtticaDetailsController(
            IAtticaDetailService atticaDetails,
            IImageHLService images,
            IMapper mapper)
        {
            this.atticaDetails = atticaDetails;
            this.images = images;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AtticaDetailFormModel atticaDetail) => View(atticaDetail);

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(AtticaDetailFormModel atticaDetail, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaDetail);
            }

            this.atticaDetails.Create(
                atticaDetail.RoofType,
                atticaDetail.IsWalkable,
                atticaDetail.ScreedWaterproofing,
                atticaDetail.Description,
                atticaDetail.ImageId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllAtticaDetailsQueryModel query)
        {
            var queryResults = this.atticaDetails.All(
                query.RoofType,
                query.IsWalkable);

            query.TotalDrains = queryResults.TotalDrains;
            query.AtticaDetails = queryResults.AtticaDetails;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var atticaDetail = this.atticaDetails.Details(id);

            var atticaDetailForm = this.mapper.Map<AtticaDetailFormModel>(atticaDetail);

            return View(atticaDetailForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, AtticaDetailFormModel atticaDetail)
        {

            if (!ModelState.IsValid)
            {
                return View(atticaDetail);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.atticaDetails.Edit(
                id,
                atticaDetail.RoofType,
                atticaDetail.IsWalkable,
                atticaDetail.ScreedWaterproofing,
                atticaDetail.Description,
                atticaDetail.ImageId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var drain = this.atticaDetails.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddImage(AtticaDetailFormModel drainCreateInfo)
        {
            drainCreateInfo.Images = this.images.GetAtticaDetailsImages(string.Empty);

            return View(drainCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddImage(int id, AtticaDetailFormModel atticaDetailCreateInfo)
        {
            var atticaDetailImage = this.images.GetImageById(id);

            if (atticaDetailImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", atticaDetailCreateInfo) });
            }

            atticaDetailCreateInfo.Image = atticaDetailImage;
            atticaDetailCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", atticaDetailCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult EditImage(int modelId, AtticaDetailFormModel atticaDetailCreateInfo)
        {
            atticaDetailCreateInfo.Images = this.images.GetAtticaDetailsImages(string.Empty);
            atticaDetailCreateInfo.Id = modelId;

            return View(atticaDetailCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult EditImage(int id, int modelId, AtticaDetailFormModel atticaDetailCreateInfo)
        {
            var atticaDetail = this.atticaDetails.Details(modelId);
            atticaDetailCreateInfo = this.mapper.Map<AtticaDetailFormModel>(atticaDetail);

            var atticaDetailImage = this.images.GetImageById(id);
            atticaDetailCreateInfo.Image = atticaDetailImage;
            atticaDetailCreateInfo.ImageId = id;

            if (atticaDetailImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", atticaDetailCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", atticaDetailCreateInfo) });
        }
    }
}