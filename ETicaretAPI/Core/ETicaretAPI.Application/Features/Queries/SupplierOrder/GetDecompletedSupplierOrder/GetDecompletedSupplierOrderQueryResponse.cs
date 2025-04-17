using ETicaretAPI.Application.DTOs.BasketItem;
using ETicaretAPI.Application.DTOs.SupplierOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.SupplierOrder.GetDecompletedSupplierOrder
{
    public class GetDecompletedSupplierOrderQueryResponse
    {
        public Guid Id { get; set; }
        public Guid OrderTrackingNumber { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid SupplierId { get; set; }
        public Guid OrderId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<ListSupplierOrderItem> SupplierOrderItems { get; set; }
    }
}
