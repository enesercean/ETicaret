using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Address;
using ETicaretAPI.Application.Repositories.Address;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class AddressService : IAddressService
    {
        readonly IAddressReadRepository _addressReadRepository;
        readonly IAddressWriteRepository _addressWriteRepository;

        public AddressService(IAddressReadRepository addressReadRepository, IAddressWriteRepository addressWriteRepository)
        {
            _addressReadRepository = addressReadRepository;
            _addressWriteRepository = addressWriteRepository;
        }

        public async Task<CreateAddress> CreateAddressAsync(CreateAddress addressDto)
        {
            var address = new Address
            {
                Id = Guid.NewGuid(),
                Name = addressDto.Name,
                UserId = addressDto.UserId,
                Street = addressDto.AddressDetail,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country,
                Phone = addressDto.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _addressWriteRepository.AddAsync(address);
            await _addressWriteRepository.SaveAsync();

            return addressDto;
        }

        public async Task<bool> DeleteAddressAsync(Guid id)
        {
            Address? address = await _addressReadRepository.GetByIdAsync(id.ToString());
            if (address == null)
                return false;

            bool result = _addressWriteRepository.Remove(address);
            await _addressWriteRepository.SaveAsync();
            return result;
        }

        public Task<Address> GetAddressByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ListAddress>> GetAddressesByUserIdAsync(Guid userId)
        {
            var addresses = await _addressReadRepository.GetWhere(a => a.UserId == userId.ToString()).ToListAsync();

            var addressDtos = addresses.Select(a => new ListAddress
            {
                Id = a.Id,
                Name = a.Name,
                PhoneNumber = a.Phone,
                AddressDetail = a.Street,
                City = a.City,
                PostalCode = a.PostalCode,
                Country = a.Country
            }).ToList();

            return addressDtos;
        }


        public Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Address> UpdateAddressAsync(UpdateAddress address)
        {
            Address? existingAddress = await _addressReadRepository.GetByIdAsync(address.Id.ToString());

            if (existingAddress == null)
                throw new Exception($"Address with ID {address.Id} not found");

            existingAddress.Name = address.Name;
            existingAddress.Street = address.AddressDetail;
            existingAddress.City = address.City;
            existingAddress.PostalCode = address.PostalCode;
            existingAddress.Country = address.Country;
            existingAddress.Phone = address.PhoneNumber;
            existingAddress.UpdatedDate = DateTime.UtcNow;

            _addressWriteRepository.Update(existingAddress);
            await _addressWriteRepository.SaveAsync();

            return existingAddress;
        }

    }
}
