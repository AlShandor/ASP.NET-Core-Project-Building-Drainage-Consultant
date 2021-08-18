namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    public class AtticaDrainsController : Controller
    {
        private readonly IAtticaDrainService atticaDrains;
        private readonly IMapper mapper;

        public AtticaDrainsController(IAtticaDrainService atticaDrains, IMapper mapper)
        {
            this.atticaDrains = atticaDrains;
            this.mapper = mapper;
        }

        public IActionResult SearchAtticaDrain(AllAtticaDrainsQueryModel allDrainsQuery)
        {
            var queryResult = this.atticaDrains.SearchAtticaDrains(
                allDrainsQuery.AtticaDetailId,
                allDrainsQuery.SearchTerm,
                allDrainsQuery.ScreedWaterproofing,
                allDrainsQuery.ConcreteWaterproofing,
                allDrainsQuery.Diameter,
                allDrainsQuery.Sorting,
                allDrainsQuery.CurrentPage,
                AllAtticaDrainsQueryModel.DrainsPerPage);

            allDrainsQuery.AtticaDetail = this.atticaDrains.GetAtticaDetailById(allDrainsQuery.AtticaDetailId);
            allDrainsQuery.TotalDrains = queryResult.TotalDrains;
            allDrainsQuery.Drains = queryResult.AtticaDrains;

            return View(allDrainsQuery);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AtticaDrainPartsDetailsModel atticaDrain, int id)
        {
            atticaDrain.AtticaDetails = this.atticaDrains.GetAtticaDetails();

            return View(atticaDrain);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(AtticaDrainPartsDetailsModel atticaDrain)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaDrain);
            }

            var drainId = this.atticaDrains.Create(
                  atticaDrain.AtticaDetailId,
                  atticaDrain.Name,
                  atticaDrain.FlowRate,
                  atticaDrain.DrainageArea,
                  atticaDrain.ScreedWaterproofing,
                  atticaDrain.ConcreteWaterproofing,
                  atticaDrain.Diameter,
                  atticaDrain.VisiblePart);

            return RedirectToAction(nameof(AddAtticaParts), new { drainId = drainId });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult All([FromQuery] AllAtticaDrainsQueryModel query)
        {
            var queryResults = this.atticaDrains.All(query.SearchTerm);

            query.Drains = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddAtticaParts(int drainId)
        {
            var aticaDrain = this.atticaDrains.Details(drainId);
            var atticaDrainForm = this.mapper.Map<AtticaDrainPartsDetailsModel>(aticaDrain);

            atticaDrainForm.AtticaParts = this.atticaDrains.GetAtticaPartsForDrain(drainId);
            atticaDrainForm.Id = aticaDrain.Id;

            return View(atticaDrainForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddAtticaParts(int id, AtticaDrainPartsDetailsModel atticaDrain)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaDrain);
            }

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var atticaDrain = this.atticaDrains.Details(id);

            if (atticaDrain == null)
            {
                return NotFound();
            }

            var atticaDrainForm = this.mapper.Map<AtticaDrainPartsDetailsModel>(atticaDrain);

            if (User.Identity.IsAuthenticated)
            {
                var isMyDrain = this.atticaDrains.IsMyAtticaDrain(id, this.User.Id());
                atticaDrainForm.IsMyAtticaDrain = isMyDrain;
            }

            return View(atticaDrainForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var aticaDrain = this.atticaDrains.Details(id);

            var atticaDrainForm = this.mapper.Map<AtticaDrainPartsDetailsModel>(aticaDrain);

            return View(atticaDrainForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Edit(int id, AtticaDrainPartsDetailsModel atticaDrain)
        {

            if (!ModelState.IsValid)
            {
                return View(atticaDrain);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.atticaDrains.Edit(
                id,
                atticaDrain.Name,
                atticaDrain.FlowRate,
                atticaDrain.DrainageArea,
                atticaDrain.ScreedWaterproofing,
                atticaDrain.ConcreteWaterproofing,
                atticaDrain.Diameter,
                atticaDrain.VisiblePart);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var atticaDrain = this.atticaDrains.Delete(id);

            if (atticaDrain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddAtticaDetail(AtticaDrainPartsDetailsModel atticaDrainsCreateInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            atticaDrainsCreateInfo.AtticaDetails = this.atticaDrains.GetAtticaDetails();

            return View(atticaDrainsCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddAtticaDetail(int id, AtticaDrainPartsDetailsModel atticaDrainsCreateInfo)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddAtticaDetail", atticaDrainsCreateInfo) });
            }

            atticaDrainsCreateInfo.AtticaDetail = this.atticaDrains.GetAtticaDetailById(id);
            atticaDrainsCreateInfo.AtticaDetailId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", atticaDrainsCreateInfo) });
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult AddAtticaPartModal(int drainId, AtticaDrainPartsDetailsModel atticaDrainsCreateInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            atticaDrainsCreateInfo.AtticaParts = this.atticaDrains.GetAtticaParts();
            atticaDrainsCreateInfo.Id = drainId;

            return View(atticaDrainsCreateInfo);
        }

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult AddAtticaPartModal(AtticaDrainPartsDetailsModel atticaDrainsCreateInfo)
        {
            var isAdded = this.atticaDrains.AddAtticaPart(atticaDrainsCreateInfo.AtticaPartId, atticaDrainsCreateInfo.Id);

            if (!ModelState.IsValid || !isAdded)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddAtticaPartModal", atticaDrainsCreateInfo) });
            }

            var aticaDrain = this.atticaDrains.Details(atticaDrainsCreateInfo.Id);
            atticaDrainsCreateInfo = this.mapper.Map<AtticaDrainPartsDetailsModel>(aticaDrain);

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddAtticaParts", atticaDrainsCreateInfo) });
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myAtticaDrains = this.atticaDrains.ByUser(this.User.Id());

            var myAtticaDrainsForm = new AllAtticaDrainsQueryModel();
            myAtticaDrainsForm.Drains = myAtticaDrains;

            return View(myAtticaDrainsForm);
        }

        [Authorize]
        public IActionResult AddToMine(int id)
        {
            var drain = this.atticaDrains.AddToMine(this.User.Id(), id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}