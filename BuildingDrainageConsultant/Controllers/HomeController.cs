namespace BuildingDrainageConsultant.Controllers
{
    using BuildingDrainageConsultant.Services.Articles;
    using BuildingDrainageConsultant.Services.Articles.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants.Cache;
    public class HomeController : Controller
    {
        private readonly IArticleService articles;
        private readonly IMemoryCache cache;

        public HomeController(
            IArticleService articles,
            IMemoryCache cache)
        {
            this.articles = articles;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestArticles = this.cache.Get<List<ArticleServiceModel>>(LatestArticlesCacheKey);

            if (latestArticles == null)
            {
                latestArticles = this.articles
                 .Latest()
                 .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(LatestArticlesCacheKey, latestArticles, cacheOptions);
            }

            return View(latestArticles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}