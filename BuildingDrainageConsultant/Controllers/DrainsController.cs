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
        public IActionResult Add(AddDrainFormModel drain)
        {
            if (!ModelState.IsValid)
            {
                return View(drain);
            }

            var drainData = new Drain
            {
                Name = drain.Name,
                FlowRate = drain.FlowRate,
                DrainageArea = drain.DrainageArea,
                Diameter = drain.Diameter,
                VisiblePart = drain.VisiblePart,
                Waterproofing = drain.Waterproofing,
                HasHeating = drain.HasHeating,
                ForRenovation = drain.ForRenovation,
                HasFlapSeal = drain.HasFlapSeal,
                ImageUrl = drain.ImageUrl == null ? DefaultImageUrl : drain.ImageUrl,
                Description = drain.Description
            };

            this.data.Drains.Add(drainData);
            this.data.SaveChanges();

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
            var drainDetails = this.data
                .Drains
                .Where(d => d.Id == id)
                .Select(d => new DrainFormModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    FlowRate = d.FlowRate,
                    DrainageArea = d.DrainageArea,
                    Diameter = d.Diameter,
                    VisiblePart = d.VisiblePart,
                    Waterproofing = d.Waterproofing,
                    HasHeating = d.HasHeating,
                    ForRenovation = d.ForRenovation,
                    HasFlapSeal = d.HasFlapSeal,
                    ImageUrl = d.ImageUrl,
                    Description = d.Description
                })
                .FirstOrDefault();

            if (drainDetails == null)
            {
                return NotFound();
            }

            return View(drainDetails);
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
