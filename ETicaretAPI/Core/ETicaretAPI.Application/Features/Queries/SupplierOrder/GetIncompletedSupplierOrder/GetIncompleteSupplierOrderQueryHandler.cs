using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.SupplierOrderItem;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.SupplierOrder.GetIncompletedSupplierOrder
{
    public class GetIncompleteSupplierOrderQueryHandler : IRequestHandler<GetIncompleteSupplierOrderQueryRequest, List<GetIncompleteSupplierOrderQueryResponse>>
    {
        readonly ISupplierOrderService _supplierOrderService;
        readonly ISupplierContactPeopleService _supplierContactPeopleService;

        public GetIncompleteSupplierOrderQueryHandler(ISupplierOrderService supplierOrderService, ISupplierContactPeopleService supplierContactPeopleService)
        {
            _supplierOrderService = supplierOrderService;
            _supplierContactPeopleService = supplierContactPeopleService;
        }

        public async Task<List<GetIncompleteSupplierOrderQueryResponse>> Handle(GetIncompleteSupplierOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var supplierContactPeople = await _supplierContactPeopleService.GetSupplierContactPeopleByUserIdAsync(request.UserId);

            var supplierIds = supplierContactPeople
                .Select(item => item.SupplierId)
                .ToList();

            var orders = await _supplierOrderService.GetIncompleteSupplierOrderBySupplierIdAsync(supplierIds);

            if (!orders.Any())
            {
                return new();
            }

            return orders.Select(x => new GetIncompleteSupplierOrderQueryResponse
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