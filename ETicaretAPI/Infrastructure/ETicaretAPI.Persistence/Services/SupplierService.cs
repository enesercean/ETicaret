using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Supplier;
using ETicaretAPI.Application.Repositories.Supplier;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class SupplierService : ISupplierService
    {
        readonly ISupplierReadRepository _supplierReadRepository;
        readonly ISupplierWriteRepository _supplierWriteRepository;

        public SupplierService(ISupplierReadRepository supplierReadRepository, ISupplierWriteRepository supplierWriteRepository)
        {
            _supplierReadRepository = supplierReadRepository;
            _supplierWriteRepository = supplierWriteRepository;
        }

        public Task AddSupplierAsync(CreateSupplier createSupplier)
        {
            var supplier = _supplierWriteRepository.AddAsync(new Domain.Entities.Supplier
            {
                Name = createSupplier.Name,
                CreatedDate = DateTime.Now,
                Id = createSupplier.Id,
                UpdatedDate = DateTime.Now
            });
            _supplierWriteRepository.SaveAsync();
            return supplier;
        }

        public async Task<ListSupplier> GetSupplierAsync(string userId)
        {
            var supplier = await _supplierReadRepository.Table
                .Include(s => s.SupplierContactPeople)
                .Where(s => s.SupplierContactPeople.Any(scp => scp.UserId == userId))
                .Select(s => new ListSupplier
                {
                    SupplierId = s.Id,
                    Name = s.Name
                })
                .FirstOrDefaultAsync();

            if (supplier == null)
            {
                return null;
            }

            return supplier;
        }

        public Task RemoveSupplierAsync(string removeSupplier)
        {
            var supplier = _supplierWriteRepository.RemoveAsync(removeSupplier);
            _supplierWriteRepository.SaveAsync();
            return supplier;
        }

        
    }
}
