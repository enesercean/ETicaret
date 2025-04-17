using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.SupplierRegistrationRequest
{
    public class CreateSupplierRegistrationRequest
    {
        public string UserId { get; set; }

        // Basic Information
        [Required(ErrorMessage = "Business name is required")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "Business type is required")]
        public string BusinessType { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        // Business Details
        [Required(ErrorMessage = "Tax ID is required")]
        public string TaxId { get; set; }

        [Required(ErrorMessage = "Business address is required")]
        public string BusinessAddress { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State/Province is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        // Product Information
        [Required(ErrorMessage = "Please select at least one product category")]
        public List<string> ProductCategories { get; set; }

        [Required(ErrorMessage = "Product description is required")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Average price range is required")]
        public string AveragePrice { get; set; }

        [Required(ErrorMessage = "Preferred shipping method is required")]
        public string ShippingMethod { get; set; }


        // Ek olarak yüklenen dosyaları işlemek için
        // Not: Dosya yükleme işlemi için ayrı bir endpoint kullanılmalı
        // bu sadece referans olarak burada
        public List<string> SampleProductImageUrls { get; set; }
    }
}
