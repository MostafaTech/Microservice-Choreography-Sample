using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MCS.Core.Contracts.Commands;
using MCS.Core.Contracts.Events;
using MassTransit;

namespace MCS.Orders
{
    public class OrderEventConsumers :
        IConsumer<OrderShippedEvent>
    {
        private readonly ILogger<OrderCommandConsumers> _logger;
        public OrderEventConsumers(ILogger<OrderCommandConsumers> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderShippedEvent> context)
        {
            _logger.LogInformation("Received Order Shipped Event: {OrderId}", context.Message.Id);

            // doing after shipping works ...

            return Task.CompletedTask;
        }
    }
}
