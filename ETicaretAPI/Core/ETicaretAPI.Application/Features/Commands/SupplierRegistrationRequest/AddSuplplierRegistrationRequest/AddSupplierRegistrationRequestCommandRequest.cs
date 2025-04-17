using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierRegistrationRequest.AddSuplplierRegistrationRequest
{
    public class AddSupplierRegistrationRequestCommandRequest : IRequest<AddSupplierRegistrationRequestCommandResponse>
    {
        public string? UserId { get; set; }

        // Basic Information
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Business Details
        public string TaxId { get; set; }
        public string BusinessAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        // Product Information
        public List<string> ProductCategories { get; set; } = new List<string>();

        public string ProductDescription { get; set; }

        public string AveragePrice { get; set; }

        public string ShippingMethod { get; set; }
    }
}
