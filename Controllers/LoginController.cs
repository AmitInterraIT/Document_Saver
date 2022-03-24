using Document_Saver.Data;
using Document_Saver.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
       
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            IActionResult response = Unauthorized();


            if (login.User_Name != null)
            {
                var tokenString = GenerateJwtToken(login);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private object GenerateJSONWebToken(Login login)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(Login obj)
        {
            var securityKey = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var claims = new Claim[] {
            new Claim(ClaimTypes.Name,obj.User_Name) };
            {

                var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);




                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

    
    }
}