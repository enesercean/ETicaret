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
    public class ProductRating : BaseEntity
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
