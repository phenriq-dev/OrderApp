using OrderApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderApp.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task CreateOrder(Order order);
    }
}
