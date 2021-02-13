using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MCS.Core.Contracts.Events;
using MassTransit;

namespace MCS.Shipping
{
    public class ShippingConsumers :
        IConsumer<WarehouseOrderAcceptedEvent>
    {
        private readonly ILogger<ShippingConsumers> _logger;
        public ShippingConsumers(ILogger<ShippingConsumers> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<WarehouseOrderAcceptedEvent> context)
        {
            _logger.LogInformation("Received Warehouse Accepted Order Event: {OrderId}", context.Message.Id);

            // doing shipping works ...

            // publish the event
            context.Publish(new OrderShippedEvent
            {
                Id = context.Message.Id
            });

            return Task.CompletedTask;
        }
    }
}
