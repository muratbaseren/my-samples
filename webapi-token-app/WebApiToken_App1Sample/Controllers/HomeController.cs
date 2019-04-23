using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using WebApiToken_App1Sample.Models;

namespace WebApiToken_App1Sample.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            var client = new RestClient("http://localhost:52289/api/auth/register");
            var request = new RestRequest("", Method.POST);

            string appname = _configuration.GetValue<string>("AppSettings:AppName");
            request.AddJsonBody(new { username, password, appname });

            IRestResponse response = client.Execute(request);
            ViewBag.Result = response.Content;

            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var client = new RestClient("http://localhost:52289/api/auth/login");
            var request = new RestRequest("", Method.POST);

            string appname = _configuration.GetValue<string>("AppSettings:AppName");
            request.AddJsonBody(new { username, password, appname });

            IRestResponse response = client.Execute(request);
            ViewBag.Result = response.Content;

            

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
