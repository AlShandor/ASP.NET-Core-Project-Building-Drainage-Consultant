namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Models.Articles;
    using BuildingDrainageConsultant.Services.Articles;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;
        private readonly IMapper mapper;

        public ArticlesController(
            IArticleService articles,
            IMapper mapper)
        {
            this.articles = articles;
            this.mapper = mapper;
        }

        public IActionResult Details(int id)
        {
            var drain = this.articles.Details(id);

            if (drain == null)
            {
                return NotFound();
            }

            var drainForm = this.mapper.Map<ArticleFormModel>(drain);

            return View(drainForm);
        }
    }
}
