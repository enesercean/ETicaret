using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR.HubService
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _productHubContext;

        public ProductHubService(IHubContext<ProductHub> productHubContext)
        {
            _productHubContext = productHubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _productHubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
