using AreaBox_V0._1.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AreaBox_V0._1.Utilities
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}
        public async Task JoinCityGroup(string city, string category)
        {
            string group = category == null ? "General" : category;
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{city}-{group}");
        }

        public async Task SendMessage(string message, string city, string category, long time)
        {
            string group = category == null ? "General" : category;
            var user = await _userManager.GetUserAsync(Context.User);
            await Clients.Group($"{city}-{group}").SendAsync("ReceiveMessage", user.Id, user.UserName, user.ProfilePicture, message, time);
        }

        public async Task LeaveCityGroup(string city, string category)
        {
            string group = category == null ? "General" : category;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{city}-{group}");
        }
    }
}
