using ETicaretAPI.Application.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ISupplierService
    {
        Task AddSupplierAsync(CreateSupplier createSupplier);
        Task RemoveSupplierAsync(string removeSupplier);
        Task<ListSupplier> GetSupplierAsync(string userId);



    }
}
