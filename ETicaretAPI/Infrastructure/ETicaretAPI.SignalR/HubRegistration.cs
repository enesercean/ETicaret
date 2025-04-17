using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Identity.Web.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR
{
    public static class HubRegistration
    {
        public static void AddHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub");
            webApplication.MapHub<OrderHub>("/orders-hub");
        }
    }
}
