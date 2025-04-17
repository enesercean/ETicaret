using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Supplier
{
    public class CreateSupplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Position { get; set; }  // Yetkili kişinin pozisyonu (Örn: Satış Müdürü)
        public string PhoneNumber { get; set; }
    }
}
