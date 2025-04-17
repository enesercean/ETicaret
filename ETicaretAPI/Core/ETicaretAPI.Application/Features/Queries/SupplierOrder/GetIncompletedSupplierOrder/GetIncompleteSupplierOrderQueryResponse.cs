using ETicaretAPI.Application.DTOs.SupplierOrderItem;

namespace ETicaretAPI.Application.Features.Queries.SupplierOrder.GetIncompletedSupplierOrder
{
    public class GetIncompleteSupplierOrderQueryResponse
    {
        public Guid Id { get; set; }
        public Guid OrderTrackingNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid SupplierId { get; set; }
        public Guid OrderId { get; set; }
        public long TotalPrice { get; set; }
        public List<ListSupplierOrderItem> SupplierOrderItems { get; set; }
    }
}
   