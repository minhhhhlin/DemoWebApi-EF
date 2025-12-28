using DemoWebAPI_2.DTO;
using DemoWebAPI_2.Helper;
using DemoWebAPI_2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebAPI_2.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;  
        public AuthController(AppDbContext context, IJwtService jwtservice)
        {
            _context = context;
            _jwtService = jwtservice;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _jwtService.Login(dto.Username, dto.Password);
            return Ok(new { token });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            _jwtService.Register(dto.Username, dto.Password);
            return Ok("User registered successfully");
        }
        
    }
}
