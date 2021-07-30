namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Drains;
    using BuildingDrainageConsultant.Services.Drains;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    using static Data.DataConstants.Drain;
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
        public IActionResult Edit(int id, DrainFormModel drainEdited)
        {
            var drainToEdit = this.data.Drains
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (drainToEdit == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                drainToEdit.Name = drainEdited.Name;
                drainToEdit.FlowRate = drainEdited.FlowRate;
                drainToEdit.DrainageArea = drainEdited.DrainageArea;
                drainToEdit.Diameter = drainEdited.Diameter;
                drainToEdit.VisiblePart = drainEdited.VisiblePart;
                drainToEdit.Waterproofing = drainEdited.Waterproofing;
                drainToEdit.HasHeating = drainEdited.HasHeating;
                drainToEdit.ForRenovation = drainEdited.ForRenovation;
                drainToEdit.HasFlapSeal = drainEdited.HasFlapSeal;
                drainToEdit.ImageUrl = drainEdited.ImageUrl;
                drainToEdit.Description = drainEdited.Description;

                this.data.SaveChanges();

                return RedirectToAction(nameof(Details), new { id = id });
            }

            return View(drainEdited);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var drainToDelete = this.data.Drains
                .Where(d => d.Id == id)
                .FirstOrDefault();

            if (drainToDelete == null)
            {
                return NotFound();
            }

            this.data.Drains.Remove(drainToDelete);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
