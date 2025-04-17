using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class SupplierRegistrationRequest : BaseEntity
    {
        // Temel Bilgiler
        public string BusinessName { get; set; } = null!;
        public string BusinessType { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        // İşletme Detayları
        public string TaxId { get; set; } = null!;
        public string BusinessAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;

        // Ürün Bilgileri
        public string ProductCategories { get; set; } = null!; // JSON olarak saklanabilir
        public string ProductDescription { get; set; } = null!;
        public string AveragePrice { get; set; } = null!;
        public string ShippingMethod { get; set; } = null!;

        // Kullanıcı Bilgisi (Form gönderen kullanıcı)
        public string CreatedById { get; set; } = null!;
        public AppUser CreatedBy { get; set; } = null!;

        // Durum Bilgisi
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
        public string? RejectionReason { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewedById { get; set; }
        public AppUser? ReviewedBy { get; set; }

        // Kayıt tamamlanma durumları
        public bool IsTransferred { get; set; } = false;
    }


    public enum RegistrationStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
