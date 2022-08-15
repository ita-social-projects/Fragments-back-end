using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Fragments.Domain.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        public static string ConnectionId { get; private set; } = string.Empty;

        public async Task EnterGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task ExitGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessageToGroup(string groopName, string message)
        {
            await Clients.Group(groopName).SendAsync("RecieveMessage", message);
        }
        public override Task OnConnectedAsync()
        {
            ConnectionId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }
    }
}
