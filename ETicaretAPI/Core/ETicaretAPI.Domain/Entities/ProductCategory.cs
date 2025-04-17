using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class ProductCategory:BaseEntity
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
