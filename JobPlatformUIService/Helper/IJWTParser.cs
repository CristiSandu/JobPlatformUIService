
namespace JobPlatformUIService.Helper
{
    public interface IJWTParser
    {
        Task<string?> GetJWT();
        Task<string?> GetUserIdFromJWT();
        Task AssignARoleToUser(string uid, string role);
        Task<bool> VerifyUserRole(string role);
    }
}