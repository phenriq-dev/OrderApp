using Microsoft.AspNetCore.Mvc;
using OrderApp.Models;
using OrderApp.Services;
using System.Threading.Tasks;

namespace OrderApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            
            var orders = await _orderService.GetOrders();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderService.CreateOrder(order);

            var orders = await _orderService.GetOrders();

            return Json(new { success = true, orders });
        }

    }
}
