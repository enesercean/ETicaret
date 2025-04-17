using System;
using System.Collections.Generic;

namespace ETicaretAPI.Application.DTOs.Brand
{
    public class CreateBrand
    {
        public string Name { get; set; }
        public List<Guid>? CategoryIds { get; set; }
    }
}
