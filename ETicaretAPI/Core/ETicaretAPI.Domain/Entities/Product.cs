using ETicaretAPI.Domain.Entities.Common;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public Guid? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public Guid? BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductLike> ProductLikes { get; set; } // Eklenen satır
        public ICollection<ProductRating> ProductRatings { get; set; } // Rating ilişkisi eklendi
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
