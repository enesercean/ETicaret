﻿using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class CompletedSupplierOrder : BaseEntity
    {
        public string OrderTrackingNumber { get; set; }
        public SupplierOrder SupplierOrder { get; set; }
    }
}
