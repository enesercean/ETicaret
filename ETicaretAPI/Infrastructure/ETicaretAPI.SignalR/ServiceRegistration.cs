using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.SignalR.HubService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR
{
    static public class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IProductHubService, ProductHubService>();
            services.AddSingleton<IOrderHubService, OrderHubService>();
        }
    }
}
