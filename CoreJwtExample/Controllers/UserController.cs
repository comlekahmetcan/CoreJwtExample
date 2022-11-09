using CoreJwtExample.IRepository;
using CoreJwtExample.Models;
using CoreJwtExample.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreJwtExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        IUserRepository _oUserRepository = null;

        public UserController(IConfiguration config, IUserRepository oUserRepository)
        {
            _config = config;
            _oUserRepository = oUserRepository;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] User model)
        {
            try
            {
                model = await _oUserRepository.Save(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Signin/{username}/{password}")]
        public async Task<IActionResult> Signin(string username, string password)
        {
            try
            {
                User model = new User()
                {
                    Username = username,
                    Password = password
                };
                var user = await AuthenticationUser(model);
                if (user.UserId == 0) return StatusCode((int)HttpStatusCode.NotFound, "Invalid user");
                user.Token = GenerateToken(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private string GenerateToken(User model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credenttials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credenttials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<User> AuthenticationUser(User user)
        {
            return await _oUserRepository.GetByUsernamePassword(user);
        }
    }
}
