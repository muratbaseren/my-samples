using DEMO.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Pages
{
    public class LogoutModel : AuthPageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }
    }
}
