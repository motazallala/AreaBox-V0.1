using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AreaBox_V0._1.Utilities
{
    public class ChatHub : Hub
    {
        public async Task JoinCityGroup(string city)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, city);
        }

        public async Task SendMessage(string message, string city, long time)
        {
            await Clients.Group(city).SendAsync("ReceiveMessage", Context.User.Identity.Name, $"{message}", time);
        }

        public async Task LeaveCityGroup(string city)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, city);
        }
    }
}
