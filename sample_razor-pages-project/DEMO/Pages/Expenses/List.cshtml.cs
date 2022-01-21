using DEMO.Entities;
using DEMO.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DEMO.Pages.Expenses
{
    public class ListModel : AuthPageModel
    {
        public List<Expense> Expenses { get; set; } = new List<Expense>();

        public void OnGet()
        {
            DatabaseContext db = new DatabaseContext();

            int userid = HttpContext.Session.GetInt32("userid").Value;
            Expenses = db.Expenses.Where(x => x.UserId == userid).ToList();
        }

        public IActionResult OnGetDelete(int id)
        {
            DatabaseContext db = new DatabaseContext();

            db.Expenses.Remove(db.Expenses.SingleOrDefault(x => x.Id == id && x.UserId == id));
            db.SaveChanges();

            return RedirectToPage("List");
        }
    }
}
