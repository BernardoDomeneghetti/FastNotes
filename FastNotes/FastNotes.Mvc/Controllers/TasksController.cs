using FastNotes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FastNotes.Mvc.Controllers
{
    public class TasksController : Controller
    {
        private readonly INoteWorker _noteWorker;

        public TasksController(INoteWorker noteworker)
        {
            _noteWorker = noteworker;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
