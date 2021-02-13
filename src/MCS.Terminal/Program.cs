using System;
using System.Threading.Tasks;
using MassTransit;

namespace MCS.Terminal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press Enter to start");
            Console.ReadLine();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
                //sbc.ReceiveEndpoint("microservice-choreography-sample", ep =>
                //{
                //    ep.Handler<Core.Contracts.Commands.PutOrder>(context =>
                //    {
                //        return Console.Out.WriteLineAsync($"Received Order: {context.Message.Id}");
                //    });
                //});
            });

            await bus.StartAsync();


            var order = new Core.Contracts.Commands.PutOrderCommand { Id = Guid.NewGuid() };
            Console.WriteLine($"Publishing a new order '{order.Id}' ...");
            await bus.Publish(order);

            
            await bus.StopAsync();
            Console.ReadLine();
        }
    }
}
