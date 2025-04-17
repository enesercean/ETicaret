using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.CompletedOrderRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.CompletedOrder.CreateCompletedOrder
{
    public class CreateCompletedOrderCommandHandler : IRequestHandler<CreateCompletedOrderCommandRequest, CreateCompletedOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly ISupplierOrderService _supplierOrderService;

        public CreateCompletedOrderCommandHandler(IOrderService orderService, ISupplierOrderService supplierOrderService)
        {
            _orderService = orderService;
            _supplierOrderService = supplierOrderService;
        }

        public async Task<CreateCompletedOrderCommandResponse> Handle(CreateCompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _supplierOrderService.CreateCompletedSupplierOrderAsync(Guid.Parse(request.CompletedSupplierOrderId), request.OrderTrackingNumber);

            await _orderService.CreateCompletedOrderAsync(request.CompletedSupplierOrderId, request.OrderTrackingNumber);

            return new();
        }
    }
}
