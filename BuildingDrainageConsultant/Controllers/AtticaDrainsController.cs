namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaParts.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AtticaDrainsController : Controller
    {
        private readonly IAtticaDrainService atticaDrains;
        private readonly IMapper mapper;

        public AtticaDrainsController(IAtticaDrainService atticaDrains, IMapper mapper)
        {
            this.atticaDrains = atticaDrains;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllAtticaDrainsQueryModel query)
        {
            var queryResults = this.atticaDrains.All(query.SearchTerm);

            query.Drains = queryResults;

            return View(query);
        }

        public IActionResult Add(AtticaDrainPartsDetailsModel atticaDrain, int id)
        {
            atticaDrain.AtticaDetails = this.atticaDrains.GetAtticaDetails();

            return View(atticaDrain);
        }

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

        public IActionResult AddAtticaParts(int drainId)
        {
            var aticaDrain = this.atticaDrains.Details(drainId);
            var atticaDrainForm = this.mapper.Map<AtticaDrainPartsDetailsModel>(aticaDrain);

            atticaDrainForm.AtticaParts = this.atticaDrains.GetAtticaPartsForDrain(drainId);
            atticaDrainForm.Id = aticaDrain.Id;

            return View(atticaDrainForm);
        }

        [HttpPost]
        public IActionResult AddAtticaParts(int id, AtticaDrainPartsDetailsModel atticaDrain)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaDrain);
            }

            return RedirectToAction(nameof(All));
        }


        public IActionResult Edit(int id)
        {
            var aticaDrain = this.atticaDrains.Details(id);

            var atticaDrainForm = this.mapper.Map<AtticaDrainPartsDetailsModel>(aticaDrain);

            return View(atticaDrainForm);
        }

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

        public IActionResult AddAtticaDetail(AtticaDrainPartsDetailsModel atticaDrainsCreateInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            atticaDrainsCreateInfo.AtticaDetails = this.atticaDrains.GetAtticaDetails();

            return View(atticaDrainsCreateInfo);
        }

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
    }
}