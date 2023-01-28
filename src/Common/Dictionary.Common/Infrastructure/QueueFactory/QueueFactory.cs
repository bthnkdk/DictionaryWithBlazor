using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Dictionary.Common.Infrastructure.QueueFactory
{
    public static class QueueFactory
    {
        public static void SendMessageToExchange(string exchangeName,
                                       string exchangeType,
                                       string queueName,
                                       object obj)
        {
            var channgel = CreateBasicConsumer()
                           .EnsureExhange(exchangeName, exchangeType)
                           .EnsureQueue(queueName, exchangeName)
                           .Model;

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
            channgel.BasicPublish(exchange: exchangeName, 
                                  routingKey: queueName,
                                  basicProperties: null,
                                  body: body);
        }

        public static EventingBasicConsumer CreateBasicConsumer()
        {
            var factory = new ConnectionFactory() { HostName = Constant.RabbitMQHost };
            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            return new EventingBasicConsumer(channel);
        }

        public static EventingBasicConsumer EnsureExhange(this EventingBasicConsumer consumer,
                                                           string exhangeName,
                                                           string exchangeType = Constant.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchange: exhangeName,
                                           type: exchangeType,
                                           durable: false,
                                           autoDelete: false);
            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                         string queueName,
                                                         string exchangeName)
        {
            consumer.Model.QueueDeclare(queue: queueName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        null);
            consumer.Model.QueueBind(queueName, exchangeName, queueName);

            return consumer;
        }
    }
}
