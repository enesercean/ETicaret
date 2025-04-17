using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Address
{
    public class CreateAddress
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string AddressDetail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
