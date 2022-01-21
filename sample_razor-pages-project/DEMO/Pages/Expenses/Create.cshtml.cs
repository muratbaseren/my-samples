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
        [Required(ErrorMessage = "{0} alan� gereklidir.")]
        [Display(Name = "Tarih")]
        public DateTime Date { get; set; }

        [BindProperty]
        [StringLength(160, ErrorMessage = "{0} alan� en fazla {1} karakter olmal�d�r.")]
        [Display(Name = "A��klama", Prompt = "Buraya bir �eyler yazabilirsiniz.")]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alan� gereklidir.")]
        [Display(Name = "Tutar", Prompt = "10,00")]
        public decimal Price { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alan� gereklidir.")]
        [StringLength(40, ErrorMessage = "{0} alan� en fazla {1} karakter olmal�d�r.")]
        [Display(Name = "Kategori", Prompt = "G�da")]
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
