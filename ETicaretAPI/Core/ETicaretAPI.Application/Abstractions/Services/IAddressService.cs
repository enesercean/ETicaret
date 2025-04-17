using ETicaretAPI.Application.DTOs.Address;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<List<ListAddress>> GetAddressesByUserIdAsync(Guid userId);
        Task<Address> GetAddressByIdAsync(Guid id);
        Task<CreateAddress> CreateAddressAsync(CreateAddress address);
        Task<Address> UpdateAddressAsync(UpdateAddress address);
        Task<bool> DeleteAddressAsync(Guid id);
    }
}
