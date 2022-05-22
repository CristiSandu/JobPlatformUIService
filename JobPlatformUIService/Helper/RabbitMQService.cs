using RabbitMQ.Client;
using System.Text;

namespace JobPlatformUIService.Helper
{
    public class RabbitMQService
    {
        public RabbitMQService()
        {

        }


        public void SendMesage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "command", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "command", basicProperties: null, body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

    }
}
