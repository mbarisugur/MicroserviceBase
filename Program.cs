using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceBase
{
    public class MicroserviceBaseClass
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to Microservice Base");
        }
        public static void Start(string Url)
        {
            var container = Startup.ConfigureService();
            var microserviceBase = container.GetRequiredService<IMicroserviceBase>();

            microserviceBase.Start(Url);
        }
    }
}