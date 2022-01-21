using WebApplication28.Context;
using WebApplication28.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebApplication28.Controllers
{
    public class CustomerController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        public ActionResult Search(string keyword)
        {
            List<Customer> customers = db.Customers
                .Where(x => 
                    x.Name.Contains(keyword) || 
                    x.Surname.Contains(keyword) || 
                    x.Birthdate.ToString().Contains(keyword) || 
                    x.Email.Contains(keyword))
                .ToList();

            return View("Index", customers);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                ToastrService.AddToUserQueue(new Toastr(message : "Customer added.", type: ToastrType.Success));

                //return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                ToastrService.AddToUserQueue(new Toastr(message: "Customer updating.", type: ToastrType.Warning));

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                ToastrService.AddToUserQueue(new Toastr(message: "Customer updated.", type: ToastrType.Success));

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();

            ToastrService.AddToUserQueue(new Toastr(message: "Customer deleted.", type: ToastrType.Error));

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
