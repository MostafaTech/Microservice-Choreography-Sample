using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MassTransit;

namespace MCS.Orders
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _bus;
        public Worker(ILoggerFactory loggerFactory, IBusControl bus)
        {
            _bus = bus;
            _logger = loggerFactory.CreateLogger<Worker>();

            //_bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            //{
            //    sbc.Host("rabbitmq://localhost");
            //    sbc.ReceiveEndpoint("microservice-choreography-sample", ep =>
            //    {
            //        ep.Consumer(() => new OrderConsumers(loggerFactory.CreateLogger<OrderConsumers>()));
            //    });
            //});
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken);
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
