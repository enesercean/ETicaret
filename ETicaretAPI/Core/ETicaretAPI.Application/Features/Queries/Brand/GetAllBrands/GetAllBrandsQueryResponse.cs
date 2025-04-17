using ETicaretAPI.Application.DTOs.Category;
using System;
using System.Collections.Generic;

namespace ETicaretAPI.Application.Features.Queries.Brand.GetAllBrands
{
    public class GetAllBrandsQueryResponse
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public List<ListCategory> Categories { get; set; }
    }

    
}
