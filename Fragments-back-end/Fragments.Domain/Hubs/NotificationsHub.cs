using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Fragments.Domain.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        private string _userId = string.Empty;
        public static string ConnectionId { get; private set; } = string.Empty;

        public async Task EnterGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task ExitGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public override Task OnConnectedAsync()
        {
            ConnectionId = Context.ConnectionId;
            if (Context.User != null)
            {
                _userId = Context.User.FindFirst(ClaimTypes.Name)!.Value;
                _ = EnterGroup(_userId);
            }
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _ = ExitGroup(_userId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
