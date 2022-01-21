using DEMO.Entities;
using DEMO.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DEMO.Pages.Expenses
{
    public class EditModel : AuthPageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "{0} alaný gereklidir.")]
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [BindProperty]
        [StringLength(160, ErrorMessage = "{0} alaný en fazla {1} karakter olmalýdýr.")]
        [Display(Name = "Açýklama", Prompt = "Buraya bir þeyler yazabilirsiniz.")]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alaný gereklidir.")]
        [Display(Name = "Tutar", Prompt = "10,00")]
        public decimal Price { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alaný gereklidir.")]
        [StringLength(40, ErrorMessage = "{0} alaný en fazla {1} karakter olmalýdýr.")]
        [Display(Name = "Kategori", Prompt = "Gýda")]
        public string Category { get; set; }

        public IActionResult OnGet(int id)
        {
            DatabaseContext db = new DatabaseContext();
            Expense expense = db.Expenses.SingleOrDefault(x => x.Id == id && x.UserId == id);

            if (expense == null)
                return RedirectToPage("List");

            Date = expense.Date.Date;
            Description = expense.Description;
            Category = expense.Category;
            Price = expense.Price;

            return Page();
        }

        public void OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();
                Expense expense = db.Expenses.SingleOrDefault(x => x.Id == id && x.UserId == id);

                expense.Date = Date;
                expense.Description = Description;
                expense.Category = Category;
                expense.Price = Price;

                if (db.SaveChanges() > 0)
                {
                    ViewData["result"] = "ok";
                }
                else
                {
                    ViewData["result"] = "error";
                }
            }
        }
    }
}
