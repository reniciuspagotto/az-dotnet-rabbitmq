using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace AzDotNetRabbitMQ.App.RabbitMQService;

public class MessageProducer : IMessageProducer
{
    public async Task SendProductMessage<T>(T message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        
        IConnection connection = await factory.CreateConnectionAsync();
        IChannel channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync("customer", false, false, false, null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var props = new BasicProperties();
        props.ContentType = "text/plain";
        props.DeliveryMode = DeliveryModes.Persistent;
        props.Expiration = "36000000";

        await channel.BasicPublishAsync("", "customer", true, props, body);
    }
}