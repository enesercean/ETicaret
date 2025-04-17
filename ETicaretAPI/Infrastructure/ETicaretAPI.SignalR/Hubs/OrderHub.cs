using ETicaretAPI.Application.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR.Hubs
{
    public class OrderHub : Hub
    {
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"User {Context.ConnectionId} added to group {groupName}");
        }

        // Gruptan çıkar
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToGroupByUnauthenticatedUserAsync(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

    }
}
