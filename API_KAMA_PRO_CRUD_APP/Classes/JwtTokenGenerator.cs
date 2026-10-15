using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API_KAMA_PRO_CRUD_APP.Classes
{
    public static class JwtTokenGenerator
    {
        private static readonly string _secretKey = "отпусти меня из омута глаз твоих, мир слишком тесен для нас двоих я не могу так ненавидеть, ни любить, то морозы лютые, то шрам в груди";
        private static readonly string _issuer = "KAMA_PRO_CRUD_APP_issuer";
        private static readonly string _audience = "KAMA_PRO_CRUD_APP_audience";
        private static readonly string _algorithm = SecurityAlgorithms.HmacSha256;


        public static string Generate(IDictionary<string, object> claims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, _algorithm);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds
            };

            if (!string.IsNullOrEmpty(_issuer))
                descriptor.Issuer = _issuer;

            if (!string.IsNullOrEmpty(_audience))
                descriptor.Audience = _audience;

            if (claims != null && claims.Count > 0)
            {
                var dict = new Dictionary<string, object>();
                foreach (var kv in claims)
                    dict[kv.Key] = kv.Value;

                descriptor.Claims = dict;
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Валидирует токен без проверки времени жизни.
        /// </summary>
        public static ClaimsPrincipal Validate(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token пустой", nameof(token));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = !string.IsNullOrEmpty(_issuer),
                ValidIssuer = _issuer,

                ValidateAudience = !string.IsNullOrEmpty(_audience),
                ValidAudience = _audience,

                ValidateLifetime = false,          // ← без проверки exp/nbf
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            var handler = new JwtSecurityTokenHandler();
            return handler.ValidateToken(token, parameters, out _);
        }
    }
}
