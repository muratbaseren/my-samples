using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication19062022.Models;

namespace WebApplication19062022.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;

        public UsersController(IJWTManagerRepository jWTManager)
        {
            _jWTManager = jWTManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var token = _jWTManager.Authenticate(new Models.User { Name = username, Password = password });

            if (token == null)
            {
                return Unauthorized();
            }

            SetJWTCookie(token.Token);
            HttpContext.Session.SetString("token", token.Token);

            return View();
        }

        private void SetJWTCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(3),
            };
            Response.Cookies.Append("auth", token, cookieOptions);
        }
    }
}