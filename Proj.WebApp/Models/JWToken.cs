using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Proj.WebApp.Models
{
    public class JWToken
    {
        private IConfiguration Configuration;

        public JWToken(IConfiguration configuration)
        {
            Configuration = configuration;

            TokenUrl = "https://localhost:5001";
            SecretKey = "TajneHasloKtoreMusiBycDoscDlugieBoInaczejSieWykrzaczy";

            TokenString = GenerateJSONWebToken();
        }

        public string TokenUrl { get; set; }
        public string SecretKey { get; set; }
        public string TokenString { get; set; }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{SecretKey}"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Name", "Bartosz"),
                new Claim(JwtRegisteredClaimNames.Email, "01153076@pw.edu.pl"),
            };

            var token = new JwtSecurityToken(
                issuer: $"{TokenUrl}",
                audience: $"{TokenUrl}",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
