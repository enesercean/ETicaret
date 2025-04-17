using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; } 
        [JsonIgnore]
        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; }
    }
}
