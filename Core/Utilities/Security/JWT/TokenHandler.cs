using Core.Extensions;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {    
        IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
            Token token = new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])); 
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            token.Expiration = DateTime.Now.AddMinutes(60); 
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer : configuration["Token:Issuer"], 
                audience: configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
            );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;   
        }
        public string CreateRefreshToken()
        {
            byte[] number = new byte[32]; 
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
        public IEnumerable<Claim> SetClaims(User user , List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddName(user.Name);
            claims.AddRoles(operationClaims.Select(p => p.Name).ToArray());
            return claims; 
        }
        
    }
}
