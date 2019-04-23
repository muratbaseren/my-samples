using System.Linq;
using System.Web.Mvc;
using WebApplication22.Context;

namespace WebApplication22.Controllers
{
    public class TodosController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Todos
        public ActionResult Index()
        {
            return View(db.TaskItems.ToList());
        }

        [HttpPost]
        public ActionResult ChangeItemState(int id, bool state)
        {
            var item = db.TaskItems.Find(id);

            if (item != null)
            {
                item.IsCompleted = state;
                db.SaveChanges();

                return Json(new { result = true });
            }

            return Json(new { result = false });
        }
    }
}