using ETicaretAPI.Application.DTOs.SupplierOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ISupplierOrderService
    {
        Task CreateSupplierOrderAsync(CreateSupplierOrder createSupplierOrder);
        Task UpdateSupplierOrderAsync(string supplierOrderId);
        Task DeleteSupplierOrderAsync(int supplierOrderId);
        Task CreateCompletedSupplierOrderAsync(Guid supplierOrderId, string orderTrackingNumber);
        Task<List<ListSupplierOrder>> GetIncompleteSupplierOrderBySupplierIdAsync(List<Guid> supplierId);
        Task<List<ListSupplierOrder>> GetDecompleteSupplierOrdersBySupplierIdAsync(List<Guid> supplierId); // Yeni fonksiyon

    }
}
