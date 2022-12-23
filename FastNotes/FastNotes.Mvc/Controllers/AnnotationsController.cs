using FastNotes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FastNotes.Mvc.Controllers
{
    public class AnnotationsController : Controller
    {
        private readonly INoteWorker _noteWorker;

        public AnnotationsController(INoteWorker noteworker)
        {
            _noteWorker = noteworker;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
