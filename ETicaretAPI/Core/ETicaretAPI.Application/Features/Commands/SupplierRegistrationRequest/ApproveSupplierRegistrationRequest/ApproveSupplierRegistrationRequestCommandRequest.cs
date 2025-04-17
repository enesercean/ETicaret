using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierRegistrationRequest.ApproveSupplierRegistrationRequest
{
    public class ApproveSupplierRegistrationRequestCommandRequest : IRequest<ApproveSupplierRegistrationRequestCommandResponse>
    {
        public string Id { get; set; } = null!;
        public bool Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
