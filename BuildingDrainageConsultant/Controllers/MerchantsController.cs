namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Merchants;
    using BuildingDrainageConsultant.Services.Merchants;
    using BuildingDrainageConsultant.Services.Merchants.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using static WebConstants.Cache;
    public class MerchantsController : Controller
    {
        private readonly IMerchantService merchants;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public MerchantsController(
            IMerchantService merchants,
            IMapper mapper,
            IMemoryCache cache)
        {
            this.merchants = merchants;
            this.mapper = mapper;
            this.cache = cache;
        }

        public IActionResult Add() => View();

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
        public IActionResult All()
        {
            var allMerchants = this.cache.Get<IEnumerable<MerchantServiceModel>>(nameof(AllMerchantsCacheKey));

            if (allMerchants == null)
            {
                allMerchants = this.merchants.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(AllMerchantsCacheKey, allMerchants, cacheOptions);
            }

            return View(allMerchants);
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