using DEMO.Entities;
using DEMO.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DEMO.Pages.Expenses
{
    public class CreateModel : AuthPageModel
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

        public void OnGet()
        {
            Date = DateTime.Now.Date;
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();
                db.Expenses.Add(new Expense
                {
                    Category = Category,
                    Date = Date,
                    Description = Description,
                    Price = Price,
                    UserId = HttpContext.Session.GetInt32("userid").Value
                });

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
