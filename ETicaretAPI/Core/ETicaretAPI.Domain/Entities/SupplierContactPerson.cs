using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class SupplierContactPerson : BaseEntity
    {
        public Guid SupplierId { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }

        public string UserId { get; set; }  // Yetkili kişi AppUser tablosundaki bir kullanıcı olacak.
        [JsonIgnore]
        public AppUser User { get; set; }

        public string Position { get; set; }  // Yetkili kişinin pozisyonu (Örn: Satış Müdürü)
        public string PhoneNumber { get; set; }
    }

}
