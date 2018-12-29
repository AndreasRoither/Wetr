using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using Wetr.Web.Responses;

namespace Wetr.Web.BL
{
    /* Some code from: https://www.codeproject.com/Tips/1208535/Create-and-Consume-JWT-Tokens-in-Csharp */
    public class JwtHelper
    {
        /* Configurable variables */
        private readonly TimeSpan Duration = TimeSpan.FromHours(1);
        private readonly string Key = "401b09eab3c954555b7a0812e013d4ca54922bb802";

        /* JwtSystem variables */
        private readonly JwtSecurityTokenHandler Handler;
        private readonly SigningCredentials SigningCredentials;
        private readonly JwtHeader Header;

        /* Inits Jwt variables */
        public JwtHelper()
        {
            this.Handler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
            this.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            this.Header = new JwtHeader(this.SigningCredentials);
        }

        /* Singleton instance */
        private static JwtHelper instance;
        public static JwtHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JwtHelper();
                }
                return instance;
            }
        }

        public class ExpiredTokenException : Exception
        {

        }

        /* Check if token is still valid */
        public bool IsValid(string token)
        {
            var jwttoken = this.Handler.ReadJwtToken(token);
            DateTime expirationDate = DateTime.Parse(jwttoken.Claims.FirstOrDefault(claim => claim.Type == "exp").Value);
            return expirationDate > DateTime.Now;
        }

        /* Extract userId from token */
        public int GetUserId(string token)
        {

            if(!IsValid(token))
                throw new ExpiredTokenException();

            var jwttoken = this.Handler.ReadJwtToken(token);
            string userIdString = jwttoken.Claims.First().Value;
            return int.Parse(userIdString);
        }

        /* Generates a new JwtToken which is used for secured routes */
        public TokenResponse Generate(int userId)
        {

            var payload = new JwtPayload
            {
                { "id ", userId},
                { "exp", DateTime.Now.Add(this.Duration).ToString()}
            };

            var secToken = new JwtSecurityToken(this.Header, payload);
            var tokenString = this.Handler.WriteToken(secToken);

            return new TokenResponse()
            {
                Token = tokenString
            };
        }
    }
}