using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.BasketItem;
using ETicaretAPI.Application.DTOs.SupplierOrderItem;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.SupplierOrder.GetDecompletedSupplierOrder
{
    public class GetDecompletedSupplierOrderQueryHandler : IRequestHandler<GetDecompletedSupplierOrderQueryRequest, List<GetDecompletedSupplierOrderQueryResponse>>
    {
        readonly ISupplierOrderService _supplierOrderService;
        readonly ISupplierContactPeopleService _supplierContactPeopleService;

        public GetDecompletedSupplierOrderQueryHandler(ISupplierOrderService supplierOrderService, ISupplierContactPeopleService supplierContactPeopleService)
        {
            _supplierOrderService = supplierOrderService;
            _supplierContactPeopleService = supplierContactPeopleService;
        }

        public async Task<List<GetDecompletedSupplierOrderQueryResponse>> Handle(GetDecompletedSupplierOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var supplierContactPeople = await _supplierContactPeopleService.GetSupplierContactPeopleByUserIdAsync(request.UserId);

            var supplierIds = supplierContactPeople
                .Select(item => item.SupplierId)
                .ToList();

            var orders = await _supplierOrderService.GetDecompleteSupplierOrdersBySupplierIdAsync(supplierIds);

            if (!orders.Any())
            {
                return new();
            }

            return orders.Select(x => new GetDecompletedSupplierOrderQueryResponse
            {
                Id = x.Id,
                OrderId = x.OrderId,
                OrderTrackingNumber = x.OrderTrackingNumber,
                Address = x.Address,
                Description = x.Description,
                IsCompleted = x.IsCompleted,
                TotalPrice = x.TotalPrice,
                SupplierOrderItems = x.ListSupplierOrderItems.Select(y => new ListSupplierOrderItem
                {
                    Id = y.Id,
                    CreatedDate = y.CreatedDate,
                    ProductId = y.ProductId,
                    Price = y.Price,
                    Quantity = y.Quantity,
                    ProductName = y.ProductName
                }).ToList(),

            }).ToList();
        }
    }

}
