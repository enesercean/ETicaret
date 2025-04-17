using ETicaretAPI.Application.DTOs.ProductRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Product
{
    public class ListProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Rating { get; set; }
        public string BrandName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<ListProductRating> Ratings { get; set; }
        public bool IsFavorite { get; set; }
    }
}
