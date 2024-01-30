using EmployeeWebApi.Model;
using EmployeeWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserAuthenticationService _authService;

       public AuthenticationController(UserAuthenticationService authService) 
        {
            _authService = authService;
        }
      
        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginModel credentials)
        {
            if (IsValidUser(credentials))
            {
                var token = _authService.GenerateToken(credentials.EmpId);
                return Ok(new { Token = token });
            }
             return Unauthorized();
        }

        private bool IsValidUser(LoginModel credentials)
        {
            return credentials.EmpId == "E00" && credentials.Password == "Akansha";
        }
    }
}
