using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AceleraPleno.API.Token
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string clienteId, string clienteSecret);
    }
    public class JWT_Token : IJWTAuthenticationManager
    {
        IDictionary<string, string> _credentials = new Dictionary<string, string>
        {
           {"abacaxi123", "segredodoabacaxi" }
        };

        private readonly string tokenKey;

        public JWT_Token(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }
        public string Authenticate(string clienteId, string clienteSecret)
        {
            if (!_credentials.Any(u => u.Key == clienteId && u.Value == clienteSecret))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, clienteId)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
