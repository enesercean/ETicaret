using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.CompletedOrder.CreateCompletedOrder
{
    public class CreateCompletedOrderCommandRequest : IRequest<CreateCompletedOrderCommandResponse>
    {
        public string CompletedSupplierOrderId { get; set; }
        public string OrderTrackingNumber { get; set; }
    }
}
