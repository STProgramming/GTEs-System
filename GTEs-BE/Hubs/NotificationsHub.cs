using Microsoft.AspNetCore.SignalR;

namespace GTEs_BE.Hubs
{
    public class NotificationsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client Connesso: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
    }
}
