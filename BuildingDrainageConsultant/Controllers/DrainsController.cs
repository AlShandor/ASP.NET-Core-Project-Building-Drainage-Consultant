namespace BuildingDrainageConsultant.Controllers
{
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

        public DrainsController(IDrainService drains, BuildingDrainageConsultantDbContext data)
        {
            this.drains = drains;
            this.data = data;
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
                .Select(d => new DrainDetailsView
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

            return View(drainDetails);
        }
    }
}
