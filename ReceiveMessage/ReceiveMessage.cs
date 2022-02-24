using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace ReceiveMessage
{
    class ReceiveMessage
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_message", type: "direct");

                var queueName = channel.QueueDeclare(queue: "messageQueue", durable: true, exclusive: false, autoDelete: false, arguments: null).QueueName;

                channel.QueueBind(queue: queueName,
                                  exchange: "direct_message",
                                  routingKey: "message");

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" Esperando por mensagens.");
                var hasMessage = channel.MessageCount(queueName) == 0;

                if (hasMessage) Console.WriteLine($"A {queueName} não possui mensagens.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {

                    try
                    {
                        byte[] body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;


                        Console.WriteLine($"Recebido: {message}");

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                    catch
                    {
                        channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                };

                channel.BasicConsume(queue: "messageQueue", autoAck: false, consumer: consumer);

                Console.WriteLine(" Pressione [enter] para sair.");
                Console.WriteLine("");
                Console.ReadLine();
            }
        }
    }
}
