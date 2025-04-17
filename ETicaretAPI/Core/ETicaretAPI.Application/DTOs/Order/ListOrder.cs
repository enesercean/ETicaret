using ETicaretAPI.Application.DTOs.BasketItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class ListOrder
    {
        public string Id { get; set; }
        public string OrderTrackingCode { get; set; }
        public string? UserName { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ListBasketItem> BasketItems { get; set; }
    }
}
