using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.BasketItem;
using ETicaretAPI.Application.Features.Queries.Order.GetAllOrder;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.CompletedOrder.GetAllCompletedOrder
{
    public class GetAllCompletedOrderQueryHandler : IRequestHandler<GetAllCompletedOrderQueryRequest, List<GetAllCompletedOrderQueryResponse>>
    {
        readonly IOrderService _orderService;

        public GetAllCompletedOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        async Task<List<GetAllCompletedOrderQueryResponse>> IRequestHandler<GetAllCompletedOrderQueryRequest, List<GetAllCompletedOrderQueryResponse>>.Handle(GetAllCompletedOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var completedOrder = await _orderService.GetAllCompletedOrderAsync();
            if (!completedOrder.Any())
            {
                return new List<GetAllCompletedOrderQueryResponse>();
            }
            var result = completedOrder.Select(o => new GetAllCompletedOrderQueryResponse
            {
                Id = o.Id,
                UserName = o.UserName,
                OrderTrackingCode = o.OrderTrackingCode,
                TotalPrice = o.TotalPrice,
                CreatedTime = o.CreatedDate,
                OrderItems = o.BasketItems.Select(bi => new ListBasketItem
                {
                    ProductId = bi.ProductId,
                    ProductName = bi.ProductName,
                    Quantity = bi.Quantity,
                    Price = bi.Price,
                    CreatedDate = bi.CreatedDate
                }).ToList()
            }).ToList();
            return result;
        }
    }
}
