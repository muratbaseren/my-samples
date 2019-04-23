using SampleMvcApp.Business.Abstract;
using SampleMvcApp.Business.Concrete;
using SampleMvcApp.Entities.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SampleMvcApp.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        //private readonly CustomerManager customerManager = new CustomerManager();
        private readonly IEntityManager<Customer> customerManager;

        public CustomerController(IEntityManager<Customer> customerManager)
        {
            this.customerManager = customerManager;
        }

        public ActionResult Index()
        {
            List<Customer> customers = customerManager.List();

            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerManager.Insert(customer);

                return RedirectToAction("Index");
            }

            return View(customer);
        }
    }
}