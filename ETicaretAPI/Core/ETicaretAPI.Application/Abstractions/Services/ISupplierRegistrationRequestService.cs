using ETicaretAPI.Application.DTOs.SupplierRegistrationRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ISupplierRegistrationRequestService
    {
        Task CreateSupplierRegistrationRequest(CreateSupplierRegistrationRequest createSupplierRegistrationRequest);
        //Task GetSupplierRegistrationRequestById(int id);
        Task<List<ListSupplierRegistrationRequest>> GetAllSupplierRegistrationRequests();
        Task<ListSupplierRegistrationRequest> ApproveSupplierRegistrationRequest(Guid id, bool status, string? rejectionReason);
        //Task UpdateSupplierRegistrationRequest(/*SupplierRegistrationRequest supplierRegistrationRequest*/);
        //Task DeleteSupplierRegistrationRequest(int id);
    }
}
