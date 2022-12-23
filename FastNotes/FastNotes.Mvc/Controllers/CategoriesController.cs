using FastNotes.Domain.Interfaces;
using FastNotes.Mvc.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FastNotes.Mvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryWorker _categoryWorker;        

        public CategoriesController(ICategoryWorker categoryWorker)
        {
            _categoryWorker = categoryWorker;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
