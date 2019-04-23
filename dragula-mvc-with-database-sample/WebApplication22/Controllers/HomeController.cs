using WebApplication22.Context;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication22.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();
            db.Customers.ToList();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
