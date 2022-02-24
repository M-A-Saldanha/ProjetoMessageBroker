using System;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using EmitMessage.viewModel;


namespace EmitMessage
{
    class EmitMessage
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "messageQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                channel.ExchangeDeclare(exchange: "direct_message",
                                    type: "direct");

                Message message = GetMessage();
                string jsonMessage = JsonSerializer.Serialize<Message>(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "direct_message", routingKey: "message", basicProperties: properties, body: body);
                Console.WriteLine("[x] Mensagem enviada: \r\n{0}", jsonMessage);
            }

            Console.WriteLine(" Pressione [enter] para sair.");
            Console.ReadLine();
        }

        private static Message GetMessage()
        {
            string name;
            string userMessage;
            Message message = new Message();

            Console.WriteLine("Escreva seu nome: ");
            name = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Nome inválido!");
                Console.WriteLine("Escreva seu nome: ");
                name = Console.ReadLine()!;
            }

            Console.WriteLine("Escreva a mensagem: ");
            userMessage = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(userMessage))
            {
                Console.WriteLine("Mensagem inválida!");
                Console.WriteLine("Escreva a mensagem: ");
                userMessage = Console.ReadLine()!;
            }

            message.Nome = name;
            message.Mensagem = userMessage;

            return message;
        }
    }
}

