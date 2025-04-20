using backend.Models;
using backend.Repository.KundeRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IKundeRepository repositories;
        private readonly IConfiguration configuration;
        public LoginController(IKundeRepository repository, IConfiguration meinconfiguration)
        {
            this.repositories = repository;
            this.configuration = meinconfiguration;

        }

        [HttpPost]
        public IActionResult Post(LoginResult user)
        {
            var mein_user = this.repositories.FindByEmailAndPasswordLoginResult(user.Email, user.Passwort);

            if (mein_user == null)
            {
                return Ok(new OperationResult("Benutzer nicht gefunden"));
            }
            var token = GenerateToken(mein_user);


            return Ok(new { Token = token, mein_user });

        }


        private string GenerateToken(LoginResult user)
        {

            var key = Encoding.ASCII.GetBytes(configuration["MeinKey"]);

            var token_handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
    {
        new Claim("id", user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Rolle)
    };

            var tk = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = token_handler.CreateToken(tk);
            return token_handler.WriteToken(token);
        }
    }
}
