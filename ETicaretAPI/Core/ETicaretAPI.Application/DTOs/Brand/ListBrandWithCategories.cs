using ETicaretAPI.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Brand
{
    public class ListBrandWithCategories
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public List<ListCategory> Categories { get; set; }
    }

}
