using System.Web.Mvc;

namespace MvcAppWithVue.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            var parser = new VueParser();

            model.VueData = parser.ParseData(model);

            return View(model);
        }
    }
}