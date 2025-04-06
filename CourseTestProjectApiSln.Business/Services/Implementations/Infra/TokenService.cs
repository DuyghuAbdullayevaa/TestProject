using CourseTestProjectApiSln.Business.DTOs.User;
using CourseTestProjectApiSln.Business.Services.Abstractions.Infra;
using CourseTestProjectApiSln.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CourseTestProjectApiSln.Business.Services.Implementations.Infra
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public TokenResponseDto GetToken(User appUser)
         {
            
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, appUser.UserName),
        new Claim(ClaimTypes.Email, appUser.Email),
        new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
        new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"])
    };

            if (appUser.UserRoles != null)
            {
                claims.AddRange(appUser.UserRoles.Select(x => new Claim(ClaimTypes.Role, x.RoleID.ToString())));
            }

           
            var expirationMinutes = Convert.ToInt32(_configuration["Jwt:ExpirationMinute"]);
            var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

            
            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow, 
                expires: expiresAt, 
                signingCredentials: signingCredentials
            );

         
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

       
            return new TokenResponseDto
            {
                Token = token,
                TokenExpirationDate = expiresAt
            };
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
             
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
    }
}
