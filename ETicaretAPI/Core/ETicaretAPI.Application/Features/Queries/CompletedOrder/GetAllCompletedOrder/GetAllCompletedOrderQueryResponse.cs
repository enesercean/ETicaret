using ETicaretAPI.Application.DTOs.BasketItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.CompletedOrder.GetAllCompletedOrder
{
    public class GetAllCompletedOrderQueryResponse 
    {
        public string Id { get; set; }
        public string OrderTrackingCode { get; set; }
        public string UserName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<ListBasketItem> OrderItems { get; set; }
    }
}
