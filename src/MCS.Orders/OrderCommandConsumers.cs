using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MCS.Core.Contracts.Commands;
using MCS.Core.Contracts.Events;
using MassTransit;

namespace MCS.Orders
{
    public class OrderCommandConsumers :
        IConsumer<PutOrderCommand>
    {
        private readonly ILogger<OrderCommandConsumers> _logger;
        public OrderCommandConsumers(ILogger<OrderCommandConsumers> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PutOrderCommand> context)
        {
            _logger.LogInformation("Received Put Order Command: {OrderId}", context.Message.Id);

            // doing order works ...

            // publish the event
            context.Publish(new OrderPutEvent
            {
                Id = context.Message.Id
            });

            return Task.CompletedTask;
        }
    }
}
