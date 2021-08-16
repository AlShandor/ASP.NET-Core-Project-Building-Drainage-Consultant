namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.AtticaDetails;
    using BuildingDrainageConsultant.Services.AtticaDetail;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;
    public class AtticaDetailsController : Controller
    {
        private readonly IAtticaDetailService atticaDetails;
        private readonly IMapper mapper;
        public AtticaDetailsController(
            IAtticaDetailService atticaDetails,
            IMapper mapper)
        {
            this.atticaDetails = atticaDetails;
            this.mapper = mapper;
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
        public IActionResult Add() => View();

        [Authorize(Roles = AdministratorRoleName)]
        [HttpPost]
        public IActionResult Add(AtticaDetailFormModel atticaDetail)
        {
            if (!ModelState.IsValid)
            {
                return View(atticaDetail);
            }

            this.atticaDetails.Create(
                atticaDetail.RoofType,
                atticaDetail.IsWalkable,
                atticaDetail.ScreedWaterproofing,
                atticaDetail.VisiblePart,
                atticaDetail.Description,
                atticaDetail.ImageUrl);

            return RedirectToAction(nameof(All));
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
                atticaDetail.VisiblePart,
                atticaDetail.Description,
                atticaDetail.ImageUrl);

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
    }
}