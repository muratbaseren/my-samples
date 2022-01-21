using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DEMO.Filters
{
    public class AuthPageModel : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("userid") == null)
            {
                context.Result = new RedirectToPageResult("/Login");
                return;
            }
        }
    }
}
