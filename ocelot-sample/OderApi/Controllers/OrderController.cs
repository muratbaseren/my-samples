using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OderApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Orders = new[]
        {
            "Order1", "Order2", "Order3"
        };

        [HttpGet]
        public ActionResult<string[]> Get()
        {
            return Orders;
        }
    }
}
