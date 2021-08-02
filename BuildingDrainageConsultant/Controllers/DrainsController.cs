namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class DrainsController : Controller
    {
        private readonly BuildingDrainageConsultantDbContext data;
        private readonly IDrainService drains;
        private readonly IMapper mapper;

        public DrainsController(
            IDrainService drains, 
            BuildingDrainageConsultantDbContext data, 
            IMapper mapper)
        {
            this.drains = drains;
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult Add() => View();

        [HttpPost]
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
                drain.Diameter,
                drain.VisiblePart,
                drain.Waterproofing,
                drain.HasHeating,
                drain.ForRenovation,
                drain.HasFlapSeal,
                drain.ImageUrl,
                drain.Description);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllDrainsQueryModel query)
        {
            var queryResult = this.drains.All(
                query.SearchTerm,
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

            return View(drain);
        }

        public IActionResult Edit(int id)
        {
            var drain = this.drains.Details(id);

            var drainForm = this.mapper.Map<DrainFormModel>(drain);

            return View(drainForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, DrainFormModel drain)
        {

            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            // todo
            //if (!User.IsAdmin())
            //{
            //    return BadRequest();
            //}

            this.drains.Edit(
                id,
                drain.Name,
                drain.FlowRate,
                drain.DrainageArea,
                drain.Diameter,
                drain.VisiblePart,
                drain.Waterproofing,
                drain.HasHeating,
                drain.ForRenovation,
                drain.HasFlapSeal,
                drain.ImageUrl,
                drain.Description);

            return RedirectToAction(nameof(All));
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var drain = this.drains.Delete(id);

            if (drain == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
