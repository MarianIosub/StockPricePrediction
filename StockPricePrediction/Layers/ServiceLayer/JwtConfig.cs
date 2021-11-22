using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainLayer;
using Microsoft.IdentityModel.Tokens;

namespace ServiceLayer
{
    public static class JwtConfig
    {
        public static string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //mockup key for now
            // TODO: move into appsettigs "Secret"
            var key = Encoding.ASCII.GetBytes("nlyCzN4W97keVeGd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}