using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR.HubService
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _orderHubContext;
        readonly IServiceProvider _serviceProvider;

        public OrderHubService(IHubContext<OrderHub> orderHubContext, IServiceProvider serviceProvider)
        {
            _orderHubContext = orderHubContext;
            _serviceProvider = serviceProvider;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
            await _orderHubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
        }

        public async Task SendMessageToSupplierUsersAsync(string supplierId, string message)
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var users = await userManager.Users.Where(u => u.SupplierContactPeople.Any(s => s.SupplierId.ToString() == supplierId)).ToListAsync();

            foreach (var user in users)
            {
                await _orderHubContext.Clients.User(user.Id).SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
                Console.WriteLine($"Message sent to user {user.Id}: {message}");
            }
        }

        public async Task AddUserToSupplierGroupAsync(string userId, string supplierId)
        {
            await _orderHubContext.Clients.User(userId).SendAsync("AddToSupplierGroup", supplierId);
        }

        public async Task RemoveUserFromSupplierGroupAsync(string userId, string supplierId)
        {
            await _orderHubContext.Clients.User(userId).SendAsync("RemoveFromSupplierGroup", supplierId);
        }
    }
}
