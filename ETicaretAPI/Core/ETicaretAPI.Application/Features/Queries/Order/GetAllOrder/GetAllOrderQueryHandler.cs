using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.BasketItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQueryRequest, List<GetAllOrderQueryResponse>>
    {
        readonly IOrderService _orderService;

        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<GetAllOrderQueryResponse>> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrderAsync();

            var result = orders.Select(o => new GetAllOrderQueryResponse
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
