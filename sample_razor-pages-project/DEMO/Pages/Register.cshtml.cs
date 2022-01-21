using DEMO.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DEMO.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "{0} alaný gereklidir.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} alaný en fazla {1} en az {2} karakter olmalýdýr.")]
        [Display(Name = "Kullanýcý Adý", Prompt = "johndoe")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alaný gereklidir.")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alaný en fazla {1} en az {2} karakter olmalýdýr.")]
        [DataType(DataType.Password)]
        [Display(Name = "Þifre", Prompt = "******")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alaný gereklidir.")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alaný en fazla {1} en az {2} karakter olmalýdýr.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "{0} alaný ile {1} alaný eþleþmiyor.")]
        [Display(Name = "Þifre(Tekrar)", Prompt = "******")]
        public string RePassword { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();

                Username = Username?.Trim().ToLower();
                User user = db.Users.FirstOrDefault(x => x.Username == Username);

                if (user == null)
                {
                    user = new User
                    {
                        Username = Username,
                        Password = Password
                    };

                    db.Users.Add(user);
                    if (db.SaveChanges() > 0)
                    {
                         return RedirectToPage("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanýcý adý zaten kullanýlýyor.");
                }
            }

            return Page();
        }
    }
}
