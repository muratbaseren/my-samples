using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiToken.Models;
using WebApiToken.Services;

namespace WebApiToken.Controllers
{
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public object GetUsers()
        {
            return _context.Users.Select(user => new
            {
                user.Id,
                user.Username,
                user.PasswordHash,
                user.PasswordSalt
            }).ToList();
        }
    }
}