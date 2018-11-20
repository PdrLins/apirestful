using ApiRestful.Data;
using System.Security.Claims;
using System;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using ApiRestful.Models;
using Microsoft.IdentityModel.Tokens;
using ApiRestful.Business.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ApiRestful.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;

        public TokenService(TokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public object CreateToken(User user)
        {
            SigningCredentials signingConfigurations = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.Key)),
                SecurityAlgorithms.HmacSha256);

            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email, "Token"),
                    new[] {
                        new Claim("ID", user.Id.ToString())

                    });

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao.AddMinutes(5);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                SigningCredentials = signingConfigurations,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);
            return token;
        }

    }

}
