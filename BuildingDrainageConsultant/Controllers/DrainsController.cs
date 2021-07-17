namespace BuildingDrainageConsultant.Controllers
{
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Models.Drains;
    using Microsoft.AspNetCore.Mvc;
    public class DrainsController : Controller
    {
        private readonly BuildingDrainageConsultantDbContext data;

        public DrainsController(BuildingDrainageConsultantDbContext data) => this.data = data;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddDrainFormModel drain)
        {
            return View();
        }
    }
}
