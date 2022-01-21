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
        [Required(ErrorMessage = "{0} alan� gereklidir.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} alan� en fazla {1} en az {2} karakter olmal�d�r.")]
        [Display(Name = "Kullan�c� Ad�", Prompt = "johndoe")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "{0} alan� gereklidir.")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alan� en fazla {1} en az {2} karakter olmal�d�r.")]
        [DataType(DataType.Password)]
        [Display(Name = "�ifre", Prompt = "******")]
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
                    ModelState.AddModelError(string.Empty, "Kullan�c� ad� ya da �ifre hatal� veya e�le�miyor.");
                }
            }

            return Page();
        }
    }
}
