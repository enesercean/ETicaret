using ETicaretAPI.Application.DTOs.Supplier;
using ETicaretAPI.Application.DTOs.SupplierAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ISupplierAddressService
    {
        Task AddSupplierAddressAsync(CreateSupplierAddress createSupplierAddress);
        Task RemoveSupplierAddressAsync(string removeSupplierAddress);
    }
}
