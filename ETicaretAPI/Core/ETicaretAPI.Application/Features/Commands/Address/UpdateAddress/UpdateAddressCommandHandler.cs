using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Address.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, UpdateAddressCommandResponse>
    {
        readonly IAddressService _addressService;

        public UpdateAddressCommandHandler(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updateAddressDto = new DTOs.Address.UpdateAddress
                {
                    Id = request.Id,
                    Name = request.Name,
                    AddressDetail = request.AddressDetail,
                    City = request.City,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    PhoneNumber = request.PhoneNumber
                };

                await _addressService.UpdateAddressAsync(updateAddressDto);

                return new UpdateAddressCommandResponse
                {
                    IsSuccess = true,
                    Message = "Address updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new UpdateAddressCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Failed to update address: {ex.Message}"
                };
            }
        }
    }
}
