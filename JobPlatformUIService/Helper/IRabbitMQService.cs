namespace JobPlatformUIService.Helper
{
    public interface IRabbitMQService
    {
        void SendMesage(string message);
    }
}