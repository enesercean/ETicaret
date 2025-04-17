using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Supplier;
using ETicaretAPI.Application.DTOs.SupplierAddress;
using ETicaretAPI.Application.Repositories.SupplierAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class SupplierAddressService : ISupplierAddressService
    {
        readonly ISupplierAddressReadRepository _supplierAddressReadRepository;
        readonly ISupplierAddressWriteRepository _supplierAddressWriteRepository;

        public SupplierAddressService(ISupplierAddressReadRepository supplierAddressReadRepository, ISupplierAddressWriteRepository supplierAddressWriteRepository)
        {
            _supplierAddressReadRepository = supplierAddressReadRepository;
            _supplierAddressWriteRepository = supplierAddressWriteRepository;
        }

        public async Task AddSupplierAddressAsync(CreateSupplierAddress createSupplierAddress)
        {
           var supplierAddress = await _supplierAddressWriteRepository.AddAsync(new Domain.Entities.SupplierAddress
           {
               Address = createSupplierAddress.Address,
               City = createSupplierAddress.City,
               Country = createSupplierAddress.Country,
               PostalCode = createSupplierAddress.PostalCode,
               SupplierId = createSupplierAddress.SupplierId,
               CreatedDate = DateTime.Now,
               Id = Guid.NewGuid(),
           });
            await _supplierAddressWriteRepository.SaveAsync();
        }

        public async Task RemoveSupplierAddressAsync(string removeSupplierAddress)
        {
            var supplierAddress = await _supplierAddressWriteRepository.RemoveAsync(removeSupplierAddress);
            await _supplierAddressWriteRepository.SaveAsync();
        }
    }
}
