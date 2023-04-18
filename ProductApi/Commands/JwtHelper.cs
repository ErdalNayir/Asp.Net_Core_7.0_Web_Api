using Microsoft.IdentityModel.Tokens;
using ProductApi.Infastructure;
using ProductApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductApi.Commands
{
    public class JwtHelper
    {
       
        public static string GetJwtToken(Seller seller)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.securityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, seller.SellerId.ToString()),
            new Claim(ClaimTypes.Email, seller.email),
            new Claim(ClaimTypes.Role, "Seller"), // Kullanıcı rolü
        };

            var token = new JwtSecurityToken(
                issuer: JwtOptions.issuer,
                audience: JwtOptions.audiance,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), // Token süresi
                signingCredentials: credentials
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }
    }
}
