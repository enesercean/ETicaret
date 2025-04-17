using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.ProductRating
{
    public class ListProductRating
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
