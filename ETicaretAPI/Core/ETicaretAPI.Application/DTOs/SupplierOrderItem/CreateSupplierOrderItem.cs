using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.SupplierOrderItem
{
    public class CreateSupplierOrderItem
    {
        public Guid SupplierOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
    }
}
