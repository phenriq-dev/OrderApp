using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrderApp.Data;
using OrderApp.Hubs;
using OrderApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrderApp.Messaging;

namespace OrderApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderConsumer> _logger;

        public OrderService(OrderDbContext context, IHubContext<OrderHub> hubContext, IPublishEndpoint publishEndpoint, ILogger<OrderConsumer> logger)
        {
            _context = context;
            _hubContext = hubContext;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order saved with ID: {order.Id}");

            if (order.Id <= 0)
            {
                _logger.LogError("Error: Invalid order ID after saving to the database!");
                return;
            }

            await _publishEndpoint.Publish<OrderCreated>(new OrderCreated
            {
                Id = order.Id,
                Product = order.Product,
                Quantity = order.Quantity,
                Status = "Pending"
            });

            _logger.LogInformation($"Published to RabbitMQ: ID={order.Id}, Product={order.Product}, Quantity={order.Quantity}, Status=Pending");

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"New order created: {order.Product}");
        }
    }
}
