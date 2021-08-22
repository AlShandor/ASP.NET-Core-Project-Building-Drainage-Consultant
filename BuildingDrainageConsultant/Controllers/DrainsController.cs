namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;
    public class DrainsController : Controller
    {
        private readonly IDrainService drains;
        private readonly IMapper mapper;

        public DrainsController(
            IDrainService drains,
            IMapper mapper)
        {
            this.drains = drains;
            this.mapper = mapper;
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Add(DrainFormModel drain)
        {
            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            this.drains.Create(
                drain.Name,
                drain.FlowRate,
                drain.DrainageArea,
                drain.Depth,
                drain.Direction,
                drain.Diameter,
                drain.VisiblePart,
                drain.Waterproofing,
                drain.Heating,
                drain.Renovation,
                drain.FlapSeal,
                drain.ImageUrl,
                drain.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllDrainsQueryModel query)
        {
            var queryResult = this.drains.All(
                query.SearchTerm,
                query.Direction,
                query.Diameter,
                query.VisiblePart,
                query.Waterproofing,
                query.Heating,
                query.Renovation,
                query.FlapSeal,
                query.Sorting,
                query.CurrentPage,
                AllDrainsQueryModel.DrainsPerPage);

            query.TotalDrains = queryResult.TotalDrains;
            query.Drains = queryResult.Drains;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var drain = this.drains.Details(id);

            if (drain == null)
            {
                return NotFound();
            }

            var drainForm = this.mapper.Map<DrainFormModel>(drain);

            if (User.Identity.IsAuthenticated)
            {
                var isMyDrain = this.drains.IsMyDrain(id, this.User.Id());
                drainForm.IsMyDrain = isMyDrain;
            }

            return View(drainForm);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var drain = this.drains.Details(id);

            var drainForm = this.mapper.Map<DrainFormModel>(drain);

            return View(drainForm);
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Edit(int id, DrainFormModel drain)
        {

            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.drains.Edit(
                id,
                drain.Name,
                drain.FlowRate,
                drain.DrainageArea,
                drain.Depth,
                drain.Direction,
                drain.Diameter,
                drain.VisiblePart,
                drain.Waterproofing,
                drain.Heating,
                drain.Renovation,
                drain.FlapSeal,
                drain.ImageUrl,
                drain.Description);

            return RedirectToAction(nameof(All));
        }


        [HttpPost]
        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id)
        {
            var drain = this.drains.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myDrains = this.drains.ByUser(this.User.Id());

            return View(myDrains);
        }

        [Authorize]
        public IActionResult AddToMine(int id)
        {
            var drain = this.drains.AddToMine(this.User.Id(), id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        public IActionResult RemoveFromMine(int id)
        {
            var userId = this.User.Id();

            var drain = this.drains.RemoveFromMine(userId, id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Mine));
        }
    }
}