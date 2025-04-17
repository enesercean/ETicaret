using ETicaretAPI.Application.DTOs.SupplierOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.SupplierOrder
{
    public class CreateSupplierOrder
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid SupplierId { get; set; }
        public string OrderTrackingNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<CreateSupplierOrderItem>? SupplierOrderItems { get; set; }
    }
}
