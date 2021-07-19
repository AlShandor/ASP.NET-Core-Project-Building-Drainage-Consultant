namespace BuildingDrainageConsultant.Controllers
{
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Models.Drains;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using static Data.DataConstants.Drain;
    public class DrainsController : Controller
    {
        private readonly BuildingDrainageConsultantDbContext data;

        public DrainsController(BuildingDrainageConsultantDbContext data) => this.data = data;

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

        public IActionResult All()
        {
            var drains = this.data
                .Drains
                .OrderByDescending(d => d.Id)
                .Select(d => new DrainListingViewModel
                {
                    Name = d.Name,
                    ImageUrl = d.ImageUrl
                })
                .ToList();

            return View(drains);
        }
    }
}
