using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string? NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Basket>? Baskets { get; set; }
        public ICollection<ProductLike>? ProductLikes { get; set; } // Eklenen satır
        public ICollection<ProductRating>? ProductRatings { get; set; }
        public ICollection<SupplierContactPerson>? SupplierContactPeople { get; set; }
        public ICollection<Address>? Addresses { get; set; } // Adres ilişkisi eklendi
    }
}
