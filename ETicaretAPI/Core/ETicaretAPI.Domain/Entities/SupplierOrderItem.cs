using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class SupplierOrderItem : BaseEntity
    {
        public Guid SupplierOrderId { get; set; }
        public SupplierOrder SupplierOrder { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
    }
}
