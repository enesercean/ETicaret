using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Payment;
using ETicaretAPI.Application.Repositories.Payment;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class PaymentSerivce : IPaymentService
    {
        readonly IPaymentReadRepository _paymentReadRepository;
        readonly IPaymentWriteRepository _paymentWriteRepository;

        public PaymentSerivce(IPaymentReadRepository paymentReadRepository, IPaymentWriteRepository paymentWriteRepository)
        {
            _paymentReadRepository = paymentReadRepository;
            _paymentWriteRepository = paymentWriteRepository;
        }

        public async Task CreatePaymentAsync(CreatePayment createPayment)
        {

            await _paymentWriteRepository.AddAsync(new Payment
            {
                OrderId = createPayment.OrderId,
                PaymentMethod = createPayment.PaymentMethod,
                CardNumber = createPayment.CardNumber,
                CardHolderName = createPayment.CardHolderName,
                ExpirationDate = createPayment.ExpirationDate,
                CVV = createPayment.CVV,
                Amount = createPayment.Amount,
                PaymentDate = createPayment.PaymentDate,
                PaymentStatus = createPayment.PaymentStatus
            });
            
            await _paymentWriteRepository.SaveAsync();

        }
    }
}
