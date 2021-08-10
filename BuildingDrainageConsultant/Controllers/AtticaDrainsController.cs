namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AtticaDrainFormModel atticaDrain)
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
    }
}
