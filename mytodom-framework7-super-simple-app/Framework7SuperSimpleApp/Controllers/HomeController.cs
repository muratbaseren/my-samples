using Framework7SuperSimpleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Framework7SuperSimpleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChangeState(int id, TodoStatus state)
        {
            var item = StaticDataSource.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return Json(new { hasError = true, message = "Item not found." });

            item.Status = state;

            return Json(new { hasError = false, message = string.Empty });
        }

        public IActionResult ChangePriority(int id, bool isImportant)
        {
            var item = StaticDataSource.Items.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return Json(new { hasError = true, message = "Item not found." });

            item.IsHighPriority = isImportant;

            return Json(new { hasError = false, message = string.Empty });
        }

        public IActionResult GetTodoList(TodoStatus state)
        {
            return PartialView("TodoListPartial", state);
        }

        [HttpPost]
        public IActionResult AddTodo(string text)
        {
            StaticDataSource.Items.Add(new TodoItem
            {
                Id = StaticDataSource.Items.Max(x => x.Id) + 1,
                IsHighPriority = false,
                Status = TodoStatus.todo,
                Text = text
            });

            return PartialView("TodoListPartial", TodoStatus.todo);
        }

        public IActionResult DeleteTodo(int id)
        {
            StaticDataSource.Items.Remove(StaticDataSource.Items.Find(x => x.Id == id));
            return Json(new { hasError = false, message = string.Empty });
        }

        [HttpPost]
        public IActionResult EditTodo(int id, string text)
        {
            StaticDataSource.Items.Find(x => x.Id == id).Text = text;
            return Json(new { hasError = false, message = string.Empty });
        }
    }
}
