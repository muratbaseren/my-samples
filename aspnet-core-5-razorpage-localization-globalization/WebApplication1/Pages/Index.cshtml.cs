using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStringLocalizer<UserCreate> _userCreateLocalizer;
        private readonly IOptions<RequestLocalizationOptions> _loc;
        public IndexModel(ILogger<IndexModel> logger, IStringLocalizer<UserCreate> userCreateLocalizer)
        {
            _logger = logger;
            _userCreateLocalizer = userCreateLocalizer;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostChangeLang(string culture, string returnUrl = "/")
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddHours(1) }
            );

            return Redirect(returnUrl);
        }
    }
}
