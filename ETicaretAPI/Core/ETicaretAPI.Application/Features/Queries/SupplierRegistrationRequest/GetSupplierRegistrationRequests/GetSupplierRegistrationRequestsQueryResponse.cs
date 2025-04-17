using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.SupplierRegistrationRequest.GetSupplierRegistrationRequests
{
    public class GetSupplierRegistrationRequestsQueryResponse
    {
        public string Id { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public string BusinessType { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string BusinessAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public List<string> ProductCategories { get; set; } = new List<string>();
        public string ProductDescription { get; set; } = null!;
        public string AvaragePrice { get; set; } = null!;
        public string ShippingMethod { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? RejectionReason { get; set; }
        public DateTime ReviewedDate { get; set; }
        public string CreatedDate { get; set; } = null!;
        public string UpdatedDate { get; set; } = null!;
        public bool IsTransfered { get; set; }
    }
}
