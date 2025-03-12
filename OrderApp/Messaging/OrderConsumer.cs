using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OrderApp.Data;
using OrderApp.Hubs;
using System;
using System.Threading.Tasks;

namespace OrderApp.Messaging
{
    public class OrderConsumer : IConsumer<OrderCreated>
    {
        private readonly OrderDbContext _context;
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(OrderDbContext context, IHubContext<OrderHub> hubContext, ILogger<OrderConsumer> logger)
        {
            _context = context;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            try
            {
                var message = context.Message;
                _logger.LogInformation($"Received order: ID={message.Id}, Product={message.Product}, Quantity={message.Quantity}, Status={message.Status}");

                var order = await _context.Orders.FindAsync(message.Id);

                if (order != null)
                {
                    order.Status = "Completed";
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Order {order.Id} updated to 'Completed'");

                        await _hubContext.Clients.All.SendAsync(
                            "ReceiveMessage"
                            , $"Order {order.Id} has been updated to '{order.Status}'!"
                            , new { id = order.Id, product = order.Product, quantity = order.Quantity, status = order.Status }
                            );

                    await context.ReceiveContext.ReceiveCompleted;
                }
                else
                {
                    _logger.LogWarning($"Order with ID {message.Id} not found in the database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing RabbitMQ message");
            }
        }
    }
}
