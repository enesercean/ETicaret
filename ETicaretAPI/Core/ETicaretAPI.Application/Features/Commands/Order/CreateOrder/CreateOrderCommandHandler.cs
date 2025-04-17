using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.SupplierOrderItem;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;
        readonly ISupplierOrderService _supplierOrderService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService, ISupplierOrderService supplierOrderService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
            _supplierOrderService = supplierOrderService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var basket = _basketService.GetUserActiveBasket;

            var orderTrackingCode = Guid.NewGuid().ToString();

            await _orderService.CreateOrderAsync(new()
            {
                Description = request.Description,
                Address = request.Address,
                BasketId = basket?.Id.ToString(),
                OrderTrackingCode = orderTrackingCode
            });

            if (basket == null)
            {
                throw new Exception("Basket Not Found");
            }

            var supplierIds = basket.BasketItems
                .Select(x => x.Product.SupplierId)
                .Distinct()
                .ToList();


            await _orderHubService.OrderAddedMessageAsync("New Order!");

            foreach (var supplierId in supplierIds)
            {//sorun
                if (supplierId == null)
                {
                    throw new Exception("SupplierId not null");
                }
                await _orderHubService.SendMessageToSupplierUsersAsync(supplierId.ToString(), "New Order!");

                Guid SupplierOrderId = Guid.NewGuid();

                await _supplierOrderService.CreateSupplierOrderAsync(new()
                {
                    Id = SupplierOrderId,
                    OrderId = basket.Id,
                    Description = request.Description,
                    SupplierId = (Guid)supplierId,
                    Address = request.Address,
                    OrderTrackingNumber = orderTrackingCode,
                    SupplierOrderItems = basket?.BasketItems
                        .Where(x => x.Product.SupplierId == supplierId)
                        .Select(x => new CreateSupplierOrderItem
                        {
                            Price = x.Product.Price,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            SupplierOrderId = basket.Id
                        }).ToList()
                });
            }

            

            return new();
        }
    }
}
