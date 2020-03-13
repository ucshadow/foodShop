using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class NavController : Controller
    {
        private readonly IProductRepository _repository;
        public NavController(IProductRepository repo)
        {
            _repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}