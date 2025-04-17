using ETicaretAPI.Application.DTOs.Payment;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(CreatePayment createPayment);
    }
}
