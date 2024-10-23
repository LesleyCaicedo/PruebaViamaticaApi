using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaViamaticaApi.Helpers
{
    /// <summary>
    /// CLASE CREADA COMO PARTE DE IMPLEMENTACION DE JWT (Token)
    /// </summary>
    public class AuthHelper
    {
        private readonly IConfiguration _configuration;

        public AuthHelper(IConfiguration configuration) => _configuration = configuration;

        public string GenerateJWTToken(string user, string role, string mail)
        {
            var claims = new List<Claim> {
                new Claim("Usuario", user),
                new Claim("Rol", role),
                new Claim("Correo", mail)
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration.GetSection("ApplicationSettings")["SecretKey"]!)
                    ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
