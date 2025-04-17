using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class SupplierOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string OrderTrackingNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<SupplierOrderItem> SupplierOrderItems { get; set; }
        public long TotalPrice { get; set; }
    }
}
