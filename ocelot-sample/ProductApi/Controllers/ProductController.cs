using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Apple", "Orange", "Banana", "Coconut"
        };

        [HttpGet]
        public ActionResult<string[]> Get()
        {
            return Products;
        }
    }
}
