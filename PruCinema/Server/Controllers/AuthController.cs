using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace PruCinema.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        TokenValidationParameters tokenValidationParameters;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login(string user, string pass)
        {
            string tokenstring = GenerateToken(user, pass);
            return Ok(tokenstring);
        }

        private string GenerateToken(string user, string pass, string secret = "")
        {
            Claim[] claims = null;
            string tokenAsString = "";

            if ((user == "usuario" && pass == "usuario") || (user == "usuario" && secret == configuration["AuthSettings:key"]))
            {
                claims = new[]
                {
                    new Claim("Email","usuario@usuario.com"),
                    //new Claim(JwtRegisteredClaimNames.UniqueName,"JABC"),
                    new Claim("User","usuario"),
                    new Claim(ClaimTypes.Role,"Administrador")
                };

                IdentityModelEventSource.ShowPII = true;
                var keybuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:key"]));
                DateTime expireTime = DateTime.Now.AddSeconds(30);
                var token = new JwtSecurityToken(issuer: configuration["AuthSettings:Issuer"], audience: configuration["AuthSettings:Audince"], claims, expires: expireTime, signingCredentials: new SigningCredentials(keybuffer, SecurityAlgorithms.HmacSha256));

                tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            }

            return tokenAsString;
        }

        [HttpGet("Refresh")]
        public IActionResult Refresh(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string newToken = "";
            try
            {
                newToken = "";

                tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AuthSettings:Audince"],
                    ValidIssuer = configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:key"])),

                };
                var pricipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgoritm(validatedToken))
                {
                    return null;
                }

                var expiryDate = long.Parse(pricipal.Claims.Single(o => o.Type == JwtRegisteredClaimNames.Exp).Value);
                var DateTimeExpire = new DateTime(expiryDate);
                var exp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate);
                
                if (exp > DateTime.UtcNow)
                {
                    // Sin expirar
                }
                else
                {
                    var user = pricipal.Claims.Single(o => o.Type == "User").Value;
                    newToken = GenerateToken(user, "", secret);
                }
            }
            catch (Exception e)
            {

            }
            return Ok(newToken);
        }

        private bool IsJwtWithValidSecurityAlgoritm(SecurityToken token)
        {
            return (token is JwtSecurityToken jwtSecurotyToken) && jwtSecurotyToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture);
        }
    }
}
