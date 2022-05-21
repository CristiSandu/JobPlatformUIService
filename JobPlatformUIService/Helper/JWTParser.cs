using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
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
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("Credentials/jobplatform-d63d7-fe5328a76935.json"),
                ProjectId = "jobplatform-d63d7",
            });
        }

        public async Task<string?> GetJWT()
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
            {
                return await _httpContextAccessor.HttpContext?.GetTokenAsync("access_token");
            }
            return null;
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

        public async Task AssignARoleToUser(string uid, string role)
        {
          

            var claims = new Dictionary<string, object>()
            {
                { "role", role },
            };
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);
        }

        public async Task<bool> VerifyUserRole(string role)
        {
            // Verify the ID token first.
            FirebaseToken decoded = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(await GetJWT());
            object isAdmin;
            if (decoded.Claims.TryGetValue("role", out isAdmin))
            {
                if ((string)isAdmin == role)
                {
                    return true;
                }
            }

            return false;

        }
    }
}
