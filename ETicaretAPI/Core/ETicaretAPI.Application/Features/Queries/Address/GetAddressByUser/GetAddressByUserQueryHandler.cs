using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Address.GetAddressByUser
{
    public class GetAddressByUserQueryHandler : IRequestHandler<GetAddressByUserQueryRequest, List<GetAddressByUserQueryResponse>>
    {
        readonly IAddressService _addressService;
        readonly IUserService _userService;
        public GetAddressByUserQueryHandler(IAddressService addressService, IUserService userService)
        {
            _addressService = addressService;
            _userService = userService;
        }
        public async Task<List<GetAddressByUserQueryResponse>> Handle(GetAddressByUserQueryRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();
            var address = await _addressService.GetAddressesByUserIdAsync(Guid.Parse(userId));

            return address.Select(a => new GetAddressByUserQueryResponse
            {
                AddressDetail = a.AddressDetail,
                City = a.City,
                Country = a.Country,
                PostalCode = a.PostalCode,      
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                Id = a.Id,
            }).ToList();

        }
    }
}
