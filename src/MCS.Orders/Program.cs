using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MassTransit;

namespace MCS.Orders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<OrderEventConsumers>();
                        x.UsingRabbitMq((bus, cfg) =>
                        {
                            cfg.Host("rabbitmq://localhost");
                            cfg.ConfigureEndpoints(bus);
                            cfg.ReceiveEndpoint("microservice-choreography-sample", ep =>
                            {
                                ep.Consumer(() => new OrderCommandConsumers(services.BuildServiceProvider().GetService<ILogger<OrderCommandConsumers>>()));
                            });
                        });
                    });
                });
    }
}
