using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Address.RemoveAddress
{
    public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommandRequest, RemoveAddressCommandResponse>
    {
        readonly IAddressService _addressService;
        public RemoveAddressCommandHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }
        public async Task<RemoveAddressCommandResponse> Handle(RemoveAddressCommandRequest request, CancellationToken cancellationToken)
        {
            await _addressService.DeleteAddressAsync(request.Id);

            return new();
        }
    }
}
