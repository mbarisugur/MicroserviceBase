using CipherService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace MicroserviceBase
{
    public interface IFireAndForgetService
    {
        void FireAndForget();
    }

    public class FireAndForgetService : IFireAndForgetService
    {

        public void FireAndForget()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://klcrkeba:JnfCJH6Kdo4H2rNQ9iM9GO-8u71VjOHv@coyote.rmq.cloudamqp.com/klcrkeba")
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("DefaultQueue", durable: true, false, false, null);
                    channel.QueueDeclare("LogQueue", durable: true, false, false, null);
                    Console.WriteLine("Custom log bekliyorum....");
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("DefaultQueue", true, consumer);

                    consumer.Received += (model, ea) =>
                    {
                        var log = Encoding.UTF8.GetString(ea.Body.ToArray());
                        var encryptedString = Security.EncryptString(log);
                        Console.WriteLine("--------------" + encryptedString);
                        var decryptedString = Security.DecryptString(encryptedString);
                        Console.WriteLine($"Çözülmüş = {decryptedString}");

                        Console.WriteLine("log alındı:" + log);
                        File.AppendAllText("logs.txt", log + "\n");

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        channel.BasicPublish("", "LogQueue", properties, ea.Body.ToArray());
                        Console.WriteLine($"log mesajı gönderilmiştir=> mesaj:{log}");

                        Console.WriteLine("loglama bitti");
                        channel.BasicAck(ea.DeliveryTag, multiple: false);
                    };
                    Console.WriteLine("Çıkış yapmak tıklayınız..");
                    Console.ReadLine();
                }
            }
        }
    }
}