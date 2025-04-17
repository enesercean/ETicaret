using ETicaretAPI.Application.DTOs.SupplierContactPeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ISupplierContactPeopleService
    {
        Task<List<ListSupplierContactPeople>> GetSupplierContactPeopleByUserIdAsync(Guid userId);
        Task AddSupplierContactPeopleAsync(CreateSupplierContactPeople createSupplierContactPeople);
        Task<List<ListSupplierContactPeople>> GetUsersForSupplierByUserIdAsync(Guid userId);
        Task DeleteSupplierContactPeopleAsync(Guid userId);
    }
}
