namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaDetail.Models;
    using BuildingDrainageConsultant.Services.AtticaDrains;
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

            query.AtticaDrains = queryResults;

            return View(query);
        }

        public IActionResult Add(AtticaDrainPartsDetailsModel atticaDrain, int id)
        {
            atticaDrain.AtticaDetails = this.atticaDrains.GetAtticaDetails();
            atticaDrain.AtticaParts = this.atticaDrains.GetAtticaParts();

            return View(atticaDrain);
        }

        [HttpPost]
        public IActionResult Add(AtticaDrainPartsDetailsModel atticaDrain)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaDrain);
            }

            this.atticaDrains.Create(
                atticaDrain.Name,
                atticaDrain.FlowRate,
                atticaDrain.DrainageArea,
                atticaDrain.ScreedWaterproofing,
                atticaDrain.ConcreteWaterproofing,
                atticaDrain.Diameter,
                atticaDrain.VisiblePart);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            var aticaDrain = this.atticaDrains.Details(id);

            var atticaDrainForm = this.mapper.Map<AtticaDrainFormModel>(aticaDrain);

            return View(atticaDrainForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, AtticaDrainFormModel atticaDrain)
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

            atticaDrainsCreateInfo.ChosenAtticaDetail = this.atticaDrains.GetAtticaDetailById(id);

            var asd = Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", atticaDrainsCreateInfo) });
            return asd;
        }
    }
}