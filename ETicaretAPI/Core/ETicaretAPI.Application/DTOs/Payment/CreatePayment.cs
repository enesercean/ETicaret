using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.Payment
{
    public class CreatePayment
    {
        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set;}
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Installment { get; set; }
        public string Email { get; set; }
        public string PaymentStatus { get; set; } = "Pending"; // Ödeme durumu (Başarılı, Başarısız, Beklemede, vs.)
    }
}
