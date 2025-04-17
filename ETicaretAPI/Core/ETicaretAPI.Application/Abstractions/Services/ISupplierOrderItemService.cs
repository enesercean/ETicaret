using ETicaretAPI.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ISupplierOrderItemService
    {
        Task<List<MostSoldProduct>> GetMostSoldProductsAsync(int count = 10);
    }
}
