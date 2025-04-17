using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.SupplierRegistrationRequest;
using ETicaretAPI.Application.Repositories.ISupplierRegistrationRequest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class SupplierRegistrationRequestService : ISupplierRegistrationRequestService
    {
        readonly ISupplierRegistrationRequestReadRepository _supplierRegistrationRequestReadRepository;
        readonly ISupplierRegistrationRequestWriteRepository _supplierRegistrationRequestWriteRepository;

        public SupplierRegistrationRequestService(ISupplierRegistrationRequestReadRepository supplierRegistrationRequestReadRepository, ISupplierRegistrationRequestWriteRepository supplierRegistrationRequestWriteRepository)
        {
            _supplierRegistrationRequestReadRepository = supplierRegistrationRequestReadRepository;
            _supplierRegistrationRequestWriteRepository = supplierRegistrationRequestWriteRepository;
        }

        public async Task<ListSupplierRegistrationRequest> ApproveSupplierRegistrationRequest(Guid id, bool status, string? rejectionReason)
        {
            var supplierRequest = await _supplierRegistrationRequestReadRepository.GetByIdAsync(id.ToString(), false);

            if (supplierRequest is null)
                throw new Exception($"Supplier registration request with ID {id} not found.");

            if (status)
            {
                supplierRequest.Status = Domain.Entities.RegistrationStatus.Approved;
                supplierRequest.RejectionReason = null;
            }
            else
            {
                supplierRequest.Status = Domain.Entities.RegistrationStatus.Rejected;

                if (string.IsNullOrWhiteSpace(rejectionReason))
                    throw new ArgumentException("Rejection reason must be provided when rejecting a request.");

                supplierRequest.RejectionReason = rejectionReason;
            }

            supplierRequest.ReviewedDate = DateTime.UtcNow;
            supplierRequest.UpdatedDate = DateTime.UtcNow;

            _supplierRegistrationRequestWriteRepository.Update(supplierRequest);
            await _supplierRegistrationRequestWriteRepository.SaveAsync();

            return
                new ListSupplierRegistrationRequest
                {
                    UserId = supplierRequest.CreatedById,
                    Id = supplierRequest.Id.ToString(),
                    BusinessName = supplierRequest.BusinessName,
                    BusinessType = supplierRequest.BusinessType,
                    FirstName = supplierRequest.FirstName,
                    LastName = supplierRequest.LastName,
                    Email = supplierRequest.Email,
                    PhoneNumber = supplierRequest.Phone,
                    TaxNumber = supplierRequest.TaxId,
                    BusinessAddress = supplierRequest.BusinessAddress,
                    City = supplierRequest.City,
                    Country = supplierRequest.Country,
                    State = supplierRequest.State,
                    PostalCode = supplierRequest.PostalCode,
                    ProductCategories = JsonSerializer.Deserialize<List<string>>(supplierRequest.ProductCategories),
                    ProductDescription = supplierRequest.ProductDescription,
                    AvaragePrice = supplierRequest.AveragePrice,
                    ShippingMethod = supplierRequest.ShippingMethod,
                    Status = supplierRequest.Status.ToString(),
                    RejectionReason = supplierRequest.RejectionReason ?? "Null",
                    ReviewedDate = supplierRequest.ReviewedDate ?? DateTime.MinValue,
                    CreatedDate = supplierRequest.CreatedDate.ToString("o"),
                    UpdatedDate = supplierRequest.UpdatedDate.ToString("o"),
                    IsTransfered = supplierRequest.IsTransferred
                };
        }


        public async Task CreateSupplierRegistrationRequest(CreateSupplierRegistrationRequest createSupplierRegistrationRequest)
        {
            try
            {
                var supplierRegistrationRequest = new Domain.Entities.SupplierRegistrationRequest
                {
                    // Temel Bilgiler
                    BusinessName = createSupplierRegistrationRequest.BusinessName,
                    BusinessType = createSupplierRegistrationRequest.BusinessType,
                    FirstName = createSupplierRegistrationRequest.FirstName,
                    LastName = createSupplierRegistrationRequest.LastName,
                    Email = createSupplierRegistrationRequest.Email,
                    Phone = createSupplierRegistrationRequest.Phone,

                    // İşletme Detayları
                    TaxId = createSupplierRegistrationRequest.TaxId,
                    BusinessAddress = createSupplierRegistrationRequest.BusinessAddress,
                    City = createSupplierRegistrationRequest.City,
                    State = createSupplierRegistrationRequest.State,
                    PostalCode = createSupplierRegistrationRequest.PostalCode,
                    Country = createSupplierRegistrationRequest.Country,

                    // Ürün Bilgileri
                    // ProductCategories'i JSON formatında saklıyoruz, daha doğru bir yaklaşım
                    ProductCategories = System.Text.Json.JsonSerializer.Serialize(createSupplierRegistrationRequest.ProductCategories),
                    ProductDescription = createSupplierRegistrationRequest.ProductDescription,
                    AveragePrice = createSupplierRegistrationRequest.AveragePrice,
                    ShippingMethod = createSupplierRegistrationRequest.ShippingMethod,

                    // Varsayılan durum değerleri
                    Status = Domain.Entities.RegistrationStatus.Pending,
                    IsTransferred = false,

                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedById = createSupplierRegistrationRequest.UserId,

                    // NOT: CreatedById'yi burada atamamız gerekiyor, 
                    // ama bu kodda mevcut kullanıcı bilgisine erişimimiz yok
                    // CreatedById = _currentUserService.GetUserId() gibi bir şey kullanmanız gerekebilir
                };

                await _supplierRegistrationRequestWriteRepository.AddAsync(supplierRegistrationRequest);
                await _supplierRegistrationRequestWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                // Hatayı logla ve tekrar fırlat
                // Gerçek bir uygulamada bir logger servisi kullanmalısınız
                throw new Exception($"Failed to create supplier registration request: {ex.Message}", ex);
            }
        }

        public async Task<List<ListSupplierRegistrationRequest>> GetAllSupplierRegistrationRequests()
        {
            var supplierRegistrationRequests = await _supplierRegistrationRequestReadRepository.GetAll(false).ToListAsync();

            var list = supplierRegistrationRequests.Select(srr => new ListSupplierRegistrationRequest
            {
                Id = srr.Id.ToString(),
                BusinessName = srr.BusinessName,
                BusinessType = srr.BusinessType,
                FirstName = srr.FirstName,
                LastName = srr.LastName,
                Email = srr.Email,
                PhoneNumber = srr.Phone,
                TaxNumber = srr.TaxId,
                BusinessAddress = srr.BusinessAddress,
                City = srr.City,
                Country = srr.Country,
                State = srr.State,
                PostalCode = srr.PostalCode,
                ProductCategories = JsonSerializer.Deserialize<List<string>>(srr.ProductCategories),
                ProductDescription = srr.ProductDescription,
                AvaragePrice = srr.AveragePrice,
                ShippingMethod = srr.ShippingMethod,
                Status = srr.Status.ToString(),
                RejectionReason = srr.RejectionReason ?? "Null",
                ReviewedDate = srr.ReviewedDate ?? DateTime.MinValue,
                CreatedDate = srr.CreatedDate.ToString("o"),
                UpdatedDate = srr.UpdatedDate.ToString("o"),
                IsTransfered = srr.IsTransferred
            }).ToList();

            return list;
        }
    }
}
