namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaParts;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class AtticaPartsController : Controller
    {
        private readonly IAtticaPartService atticaParts;
        private readonly IMapper mapper;

        public AtticaPartsController(
            IAtticaPartService parts,
            IMapper mapper)
        {
            this.atticaParts = parts;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(AtticaPartFormModel atticaPart)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaPart);
            }

            this.atticaParts.Create(
                atticaPart.Name,
                atticaPart.ImageUrl,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllAtticaPartsQueryModel query)
        {
            var queryResults = this.atticaParts.All(query.SearchTerm);

            query.AtticaParts = queryResults;

            return View(query);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var drain = this.atticaParts.Details(id);

            var drainForm = this.mapper.Map<AtticaPartFormModel>(drain);

            return View(drainForm);
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
                atticaPart.ImageUrl,
                atticaPart.Description);

            return RedirectToAction(nameof(All));
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
    }
}