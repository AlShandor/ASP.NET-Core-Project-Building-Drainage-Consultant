namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using Microsoft.AspNetCore.Mvc;
    public class AtticaPartsController : Controller
    {
        private readonly IAtticaPartService parts;
        private readonly IMapper mapper;

        public AtticaPartsController(
            IAtticaPartService parts,
            IMapper mapper)
        {
            this.parts = parts;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllAtticaPartsQueryModel query)
        {
            var queryResults = this.parts.All(query.SearchTerm);

            query.AtticaParts = queryResults;

            return View(query);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AtticaPartFormModel atticaPart)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaPart);
            }

            this.parts.Create(
                atticaPart.Name,
                atticaPart.ImageUrl,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            var drain = this.parts.Details(id);

            var drainForm = this.mapper.Map<AtticaPartFormModel>(drain);

            return View(drainForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, AtticaPartFormModel part)
        {

            if (!ModelState.IsValid)
            {
                return View(part);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.parts.Edit(
                id,
                part.Name,
                part.ImageUrl,
                part.Description);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var drain = this.parts.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
