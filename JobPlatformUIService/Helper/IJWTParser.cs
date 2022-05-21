
namespace JobPlatformUIService.Helper
{
    public interface IJWTParser
    {
        Task<string?> GetUserIdFromJWT();
        Task GetUserIdFromJWT(string uid, string role);
    }
}