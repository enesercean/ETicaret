using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Address.CreateAddress
{
    public class CreateAddressCommandRequest : IRequest<CreateAddressCommandResponse>
    {
        public string Name { get; set; }
        public string AddressDetail { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string? UserId { get; set; } = string.Empty;
        public string Country { get; set; }
    }
}
