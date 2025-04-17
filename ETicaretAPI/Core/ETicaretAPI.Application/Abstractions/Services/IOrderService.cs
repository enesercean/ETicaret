using ETicaretAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
        Task<List<ListOrder>> GetAllOrderAsync();
        Task CreateCompletedOrderAsync(string CompletedOrderId,string orderTrackingNumber);
        Task<List<ListOrder>> GetAllCompletedOrderAsync();
        Task<List<ListOrder>> GetOrdersByUserAsync(string userId);
    }
}
