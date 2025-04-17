using ETicaretAPI.Application.DTOs.BasketItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrdersByUser
{
    public class GetOrdersByUserQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OrderTrackingCode { get; set; }
        public float TotalPrice { get; set; }
        public List<ListBasketItem> BasketItems { get; set; }
    }
}
