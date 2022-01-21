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

        [BindProperty]
        [Required(ErrorMessage = "{0} alan� gereklidir.")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "{0} alan� en fazla {1} en az {2} karakter olmal�d�r.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "{0} alan� ile {1} alan� e�le�miyor.")]
        [Display(Name = "�ifre(Tekrar)", Prompt = "******")]
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
                    ModelState.AddModelError(string.Empty, "Kullan�c� ad� zaten kullan�l�yor.");
                }
            }

            return Page();
        }
    }
}
