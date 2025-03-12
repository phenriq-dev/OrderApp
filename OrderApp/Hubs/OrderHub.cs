using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrderApp.Hubs
{
    public class OrderHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
