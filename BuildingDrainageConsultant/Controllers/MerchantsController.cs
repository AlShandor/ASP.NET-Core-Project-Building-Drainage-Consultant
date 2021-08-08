namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Services.Merchants;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class MerchantsController : Controller
    {
        private readonly IMerchantService merchants;
        private readonly IMapper mapper;

        public MerchantsController(
            IMerchantService merchants,
            IMapper mapper)
        {
            this.merchants = merchants;
            this.mapper = mapper;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(MerchantFormModel merchant)
        {
            if (!ModelState.IsValid)
            {
                return View(merchant);
            }

            this.merchants.Create(
                merchant.Name,
                merchant.City,
                merchant.Address,
                merchant.Email,
                merchant.Phone,
                merchant.Website,
                merchant.Latitude,
                merchant.Longitude);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(IEnumerable<MerchantServiceModel> merchantsQuery)
        {
            merchantsQuery = this.merchants.All(merchantsQuery);

            return View(merchantsQuery);
        }

        public IActionResult Edit(int id)
        {
            var merchant = this.merchants.Details(id);

            var merchantForm = this.mapper.Map<MerchantFormModel>(merchant);

            return View(merchantForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, MerchantFormModel merchant)
        {

            if (!ModelState.IsValid)
            {
                return View(merchant);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.merchants.Edit(
                id,
                merchant.Name,
                merchant.City,
                merchant.Address,
                merchant.Email,
                merchant.Phone,
                merchant.Website,
                merchant.Latitude,
                merchant.Longitude);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var merchant = this.merchants.Delete(id);

            if (merchant == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
