using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.BasketItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrdersByUser
{
    public class GetOrdersByUserQueryHandler : IRequestHandler<GetOrdersByUserQueryRequest, List<GetOrdersByUserQueryResponse>>
    {
        readonly IOrderService _orderService;
        readonly IUserService _userService;

        public GetOrdersByUserQueryHandler(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<List<GetOrdersByUserQueryResponse>> Handle(GetOrdersByUserQueryRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();

            var orders = await _orderService.GetOrdersByUserAsync(userId);
            
            return orders.Select(o => new GetOrdersByUserQueryResponse
            {
                Id = Guid.Parse(o.Id),
                CreatedDate = o.CreatedDate,
                OrderTrackingCode = o.OrderTrackingCode.ToString(),
                TotalPrice = o.TotalPrice,
                BasketItems = o.BasketItems.Select(bi => new ListBasketItem
                {
                    ProductId = bi.ProductId,
                    ProductName = bi.ProductName,
                    Quantity = bi.Quantity,
                    Price = bi.Price,
                    CreatedDate = bi.CreatedDate
                }).ToList()
            }).ToList();
        }
    }
}
