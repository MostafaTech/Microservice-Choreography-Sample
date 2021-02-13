using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MCS.Core.Contracts.Events;
using MassTransit;

namespace MCS.Warehouse
{
    public class WarehouseConsumers :
        IConsumer<OrderPutEvent>
    {
        private readonly ILogger<WarehouseConsumers> _logger;
        public WarehouseConsumers(ILogger<WarehouseConsumers> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderPutEvent> context)
        {
            _logger.LogInformation("Received Order Put Event: {OrderId}", context.Message.Id);

            // doing warehouse work ...

            // publish the event
            context.Publish(new WarehouseOrderAcceptedEvent
            {
                Id = context.Message.Id
            });

            return Task.CompletedTask;
        }
    }
}
