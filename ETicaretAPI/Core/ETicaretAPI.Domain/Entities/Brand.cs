﻿using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }

}
