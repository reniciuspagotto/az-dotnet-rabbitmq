namespace AzDotNetRabbitMQ.App.RabbitMQService;

public interface IMessageProducer
{
    Task SendProductMessage<T>(T message);
}