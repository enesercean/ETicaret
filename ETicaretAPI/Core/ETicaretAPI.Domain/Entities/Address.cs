using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;
using System.Text.Json.Serialization;

namespace ETicaretAPI.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }
    }
}
