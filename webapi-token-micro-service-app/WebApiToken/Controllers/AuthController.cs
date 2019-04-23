using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiToken.DataTransferObjects;
using WebApiToken.Models;
using WebApiToken.Services;

namespace WebApiToken.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private IAppsService _appsService;

        public AuthController(IAuthService authService, IConfiguration configuration, IMapper mapper, IAppsService appsService)
        {
            _authService = authService;
            _configuration = configuration;
            _mapper = mapper;
            _appsService = appsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegistrationDto userForRegistrationDto)
        {
            if (await _authService.IsUserExists(userForRegistrationDto.Username, userForRegistrationDto.AppName))
            {
                ModelState.AddModelError(nameof(userForRegistrationDto.Username), "Username already exists.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userForRegistrationDto);

            var createdUser = await _authService.Register(user, userForRegistrationDto.Password, userForRegistrationDto.AppName);

            if (createdUser == null) return StatusCode(501, "User couldn't registered.");

            return StatusCode(201, new { userid = user.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            User user = await _authService.Login(userForLoginDto.Username, userForLoginDto.Password, userForLoginDto.AppName);

            if (user == null) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var appkey = Encoding.ASCII.GetBytes(_appsService.GetAppKeyByAppName(userForLoginDto.AppName));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddMinutes(5),
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Actor, user.AppName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(appkey), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}