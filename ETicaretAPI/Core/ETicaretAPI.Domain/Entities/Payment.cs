using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        public string PaymentMethod { get; set; } // Ödeme yöntemi (Kredi Kartı, Banka Havalesi, vs.)
        public string CardNumber { get; set; } // Kredi kartı numarası (Maskelenmiş olarak tutulmalı)
        public string CardHolderName { get; set; } // Kart sahibi adı
        public DateTime ExpirationDate { get; set; } // Son kullanma tarihi
        public string CVV { get; set; } // Güvenlik kodu (Maskelenmiş olarak tutulmalı)
        public decimal Amount { get; set; } // Ödeme tutarı
        public DateTime PaymentDate { get; set; } // Ödeme tarihi
        public string PaymentStatus { get; set; } // Ödeme durumu (Başarılı, Başarısız, Beklemede, vs.)
    }
}
