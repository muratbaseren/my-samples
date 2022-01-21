using DEMO.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DEMO.Pages
{
    public class LoginModel : PageModel
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

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DatabaseContext db = new DatabaseContext();

                Username = Username?.Trim().ToLower();
                User user = db.Users.FirstOrDefault(x => x.Username == Username && x.Password == Password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("userid", user.Id);
                    HttpContext.Session.SetString("namesurname", user?.NameSurname ?? string.Empty);
                    HttpContext.Session.SetString("username", user.Username);

                    return RedirectToPage("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanýcý adý ya da þifre hatalý veya eþleþmiyor.");
                }
            }

            return Page();
        }
    }
}
