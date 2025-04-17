using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Address.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, CreateAddressCommandResponse>
    {
        readonly IAddressService _addressService;
        readonly IUserService _userService;

        public CreateAddressCommandHandler(IAddressService addressService, IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }

        public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();

            request.UserId = userId;

            var address = await _addressService.CreateAddressAsync(new DTOs.Address.CreateAddress
            {
                UserId = userId,
                Name = request.Name,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                State = request.AddressDetail,
                AddressDetail = request.AddressDetail,
                PhoneNumber = request.PhoneNumber
            });

            return new();
        }
    }
}
