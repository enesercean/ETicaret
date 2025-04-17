using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.SupplierContactPeople;
using ETicaretAPI.Application.Repositories.SupplierContactPerson;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class SupplierContactPeopleService : ISupplierContactPeopleService
    {
        readonly ISupplierContactPeopleReadRepository _supplierContactPeopleReadRepository;
        readonly ISupplierContactPeopleWriteRepository _supplierContactPeopleWriteRepository;

        public SupplierContactPeopleService(ISupplierContactPeopleWriteRepository supplierContactPeopleWriteRepository, ISupplierContactPeopleReadRepository supplierContactPeopleReadRepository)
        {
            _supplierContactPeopleWriteRepository = supplierContactPeopleWriteRepository;
            _supplierContactPeopleReadRepository = supplierContactPeopleReadRepository;
        }

        public async Task AddSupplierContactPeopleAsync(CreateSupplierContactPeople createSupplierContactPeople)
        {
            SupplierContactPerson supplierContactPerson = new SupplierContactPerson()
            {
                Id = new Guid(),
                SupplierId = createSupplierContactPeople.SupplierId,
                UserId = createSupplierContactPeople.UserId.ToString(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Position = "SupplierManager",
                PhoneNumber = createSupplierContactPeople.PhoneNumber
            };

            await _supplierContactPeopleWriteRepository.AddAsync(supplierContactPerson);
            await _supplierContactPeopleWriteRepository.SaveAsync();
        }

        public async Task DeleteSupplierContactPeopleAsync(Guid userId)
        {
            // Kullanıcı ID'sine göre tedarikçi iletişim kişisini bul
            var supplierContactPersons = await _supplierContactPeopleReadRepository
                .GetWhere(scp => scp.UserId == userId.ToString())
                .ToListAsync();

            if (!supplierContactPersons.Any())
            {
                throw new Exception($"No supplier contact person found for user ID: {userId}");
            }

            // Bulunan tüm kayıtları kaldır
            foreach (var contactPerson in supplierContactPersons)
            {
                _supplierContactPeopleWriteRepository.Remove(contactPerson);
            }

            // Değişiklikleri kaydet
            await _supplierContactPeopleWriteRepository.SaveAsync();
        }


        public async Task<List<ListSupplierContactPeople>> GetSupplierContactPeopleByUserIdAsync(Guid userId)
        {
            var supplierContactPeople = await _supplierContactPeopleReadRepository.GetWhere(scp => scp.UserId == userId.ToString()).ToListAsync();

            return supplierContactPeople.Select(scp => new ListSupplierContactPeople
            {
                Id = scp.Id,
                SupplierId = scp.SupplierId,
                UserId = Guid.Parse(scp.UserId)
            }).ToList();
        }

        public async Task<List<ListSupplierContactPeople>> GetUsersForSupplierByUserIdAsync(Guid userId)
        {
            // Find the supplier(s) this user belongs to
            var userSuppliers = await _supplierContactPeopleReadRepository
                .GetWhere(scp => scp.UserId == userId.ToString())
                .Select(scp => scp.SupplierId)
                .ToListAsync();

            if (!userSuppliers.Any())
                return new List<ListSupplierContactPeople>();

            // Get all users belonging to those suppliers
            var supplierUsers = await _supplierContactPeopleReadRepository
                .GetWhere(scp => userSuppliers.Contains(scp.SupplierId))
                .ToListAsync();

            return supplierUsers.Select(scp => new ListSupplierContactPeople
            {
                Id = scp.Id,
                SupplierId = scp.SupplierId,
                UserId = Guid.Parse(scp.UserId)
            }).ToList();
        }
    }
}
