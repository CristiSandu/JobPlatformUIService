using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace JobPlatformUIService.Helper
{
    public class JWTParser : IJWTParser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JWTParser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string?> GetUserIdFromJWT()
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
            {
                var jwt = await _httpContextAccessor.HttpContext?.GetTokenAsync("access_token");
                var handler = new JwtSecurityTokenHandler();
                var token2 = handler.ReadJwtToken(jwt);
                return token2.Payload["user_id"].ToString();
            }
            return null;
        }

        public async Task GetUserIdFromJWT(string uid, string role)
        {
            var claims = new Dictionary<string, object>()
            {
                { role, true },
            };
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);
        }
    }
}
