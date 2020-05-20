using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsoleListener
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "172.20.10.10", Port = 5672 };
            Console.WriteLine("Message check 'RabbitMQ'");
            
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: "lesson_start", durable: true, exclusive: false, autoDelete: false);

                var consumer = new EventingBasicConsumer(channel);


                consumer.Received += (eventModel, arg) =>
                {
                    var message = Encoding.UTF8.GetString(arg.Body.ToArray());

                    Console.WriteLine("Received message: " + message);


                };


                while (true)
                {
                    channel.BasicConsume(queue: "lesson_start", autoAck: true, consumer: consumer);
                }
            }
            
            

           
        }
    }
}
