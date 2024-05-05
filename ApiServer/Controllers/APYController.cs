using ApiServer.Models;
using ApiServer.TokenData;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    public class APIMutations
    {
        [GraphQLMutation("Get JWT Token")]
        public Expression<Func<olympicsContext, JWTToken>> GetToken(olympicsContext db, string login, string password)
        {
            var identity = GetIdentity(db, login, password);
            if (identity == null)
                return (ctx) => null;
            API_Person person = db.API_Persons.FirstOrDefault(x => x.Login == login && x.Password == password);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var sk = AuthOptions.ExtendKeyLengthIfNeeded(AuthOptions.GetSymmetricSecurityKey(), 32);
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(sk, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var api_person = new JWTToken();
            api_person.Login = person.Login;
            api_person.Token = "Bearer " + encodedJwt;
            api_person.Role = person.Role;
            return (ctx) => api_person;
        }

        private ClaimsIdentity GetIdentity(olympicsContext db, string username, string password)
        {
            API_Person person = db.API_Persons.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

    }
}
