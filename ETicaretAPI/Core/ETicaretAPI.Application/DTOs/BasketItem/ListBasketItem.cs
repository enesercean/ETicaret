using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.BasketItem
{
    public class ListBasketItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
