using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.SupplierRegistrationRequest
{
    public class ListSupplierRegistrationRequest
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }
        public string BusinessAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public List<string> ProductCategories { get; set; }
        public string ProductDescription { get; set; }
        public string AvaragePrice { get; set; }
        public string ShippingMethod { get; set; }
        public string Status { get; set; }
        public string RejectionReason { get; set; }
        public DateTime ReviewedDate { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsTransfered { get; set; }
    }
}
