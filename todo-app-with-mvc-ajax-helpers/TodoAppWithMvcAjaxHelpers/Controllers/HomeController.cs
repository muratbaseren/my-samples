using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TodoAppWithMvcAjaxHelpers.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(Todo model)
        {
            System.Threading.Thread.Sleep(2000);

            if (ModelState.IsValid)
            {
                db.Todos.Add(model);
                db.SaveChanges();

                ViewBag.State = "done";
                ViewBag.Message = "Görev kaydedilmiştir.";
            }
            else
            {
                ViewBag.State = "fail";
                ViewBag.Message = "Hata oluştu.";
            }

            return PartialView("_Create", model);
        }

        public ActionResult List()
        {
            System.Threading.Thread.Sleep(2000);

            return PartialView("_List", db.Todos.ToList());
        }

        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", db.Todos.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Todo model)
        {
            System.Threading.Thread.Sleep(2000);

            if (ModelState.IsValid)
            {
                Todo todo = db.Todos.Find(id);
                todo.Text = model.Text;
                todo.IsCompleted = model.IsCompleted;
                todo.DueDate = model.DueDate;

                db.SaveChanges();

                ViewBag.State = "done";
                ViewBag.Message = "Görev güncellenmiştir.";
            }
            else
            {
                ViewBag.State = "fail";
                ViewBag.Message = "Hata oluştu.";
            }

            return PartialView("_Edit", model);
        }

        public ActionResult Details(int id)
        {
            return PartialView("_Details", db.Todos.Find(id));
        }

        public ActionResult Delete(int id)
        {
            System.Threading.Thread.Sleep(2000);

            db.Todos.Remove(db.Todos.Find(id));
            db.SaveChanges();

            return List();
        }
    }
}