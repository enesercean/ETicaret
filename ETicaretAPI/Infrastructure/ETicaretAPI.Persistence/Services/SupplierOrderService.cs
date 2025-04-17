using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.SupplierContactPeople;
using ETicaretAPI.Application.DTOs.SupplierOrder;
using ETicaretAPI.Application.DTOs.SupplierOrderItem;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.CompletedSupplierOrder;
using ETicaretAPI.Application.Repositories.SupplierOrder;
using ETicaretAPI.Application.Repositories.SupplierOrderItem;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Repositories.SupplierOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class SupplierOrderService : ISupplierOrderService
    {
        readonly ISupplierOrderReadRepository _supplierOrderReadRepository;
        readonly ISupplierOrderWriteRepository _supplierOrderWriteRepository;
        readonly ICompletedSupplierOrderWriteRepository _completedSupplierOrderWriteRepository;

        public SupplierOrderService(ISupplierOrderReadRepository supplierOrderReadRepository, ISupplierOrderWriteRepository supplierOrderWriteRepository, ICompletedSupplierOrderWriteRepository completedSupplierOrderWriteRepository)
        {
            _supplierOrderReadRepository = supplierOrderReadRepository;
            _supplierOrderWriteRepository = supplierOrderWriteRepository;
            _completedSupplierOrderWriteRepository = completedSupplierOrderWriteRepository;
        }

        public async Task CreateSupplierOrderAsync(CreateSupplierOrder createSupplierOrder)
        {
            if (createSupplierOrder.SupplierOrderItems == null)
            {
                throw new Exception("Order item Not Found");
            }

            await _supplierOrderWriteRepository.AddAsync(new()
            {
                Id = createSupplierOrder.Id,
                IsCompleted = false,
                CreatedDate = DateTime.Now,
                OrderId = createSupplierOrder.OrderId,
                Address = createSupplierOrder.Address,
                Description = createSupplierOrder.Description,
                OrderTrackingNumber = createSupplierOrder.OrderTrackingNumber,
                SupplierId = createSupplierOrder.SupplierId,
                SupplierOrderItems = createSupplierOrder.SupplierOrderItems.Select(x => new SupplierOrderItem
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    SupplierOrderId = createSupplierOrder.Id
                }).ToList(),
                TotalPrice = createSupplierOrder.SupplierOrderItems.Sum(x => x.Price * x.Quantity)
            });
            
            await _supplierOrderWriteRepository.SaveAsync();
        }

        public Task DeleteSupplierOrderAsync(int supplierOrderId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSupplierOrderAsync(string supplierOrderId)
        {
            var supplierOrder = await _supplierOrderReadRepository.GetByIdAsync(supplierOrderId);
            await _supplierOrderWriteRepository.SaveAsync();
        }

        public async Task CreateCompletedSupplierOrderAsync(Guid supplierOrderId, string orderTrackingNumber)
        {

            var supplierOrder = await _supplierOrderReadRepository.GetByIdAsync(supplierOrderId.ToString());
            if (supplierOrder == null)
            {
                throw new Exception("Supplier Order Not Found");
            }

            var completedSupplierOrder = new CompletedSupplierOrder
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                OrderTrackingNumber = orderTrackingNumber,
                SupplierOrder = supplierOrder
            };

            await _completedSupplierOrderWriteRepository.AddAsync(completedSupplierOrder);

            supplierOrder.IsCompleted = true;
            _supplierOrderWriteRepository.Update(supplierOrder);

            await _supplierOrderWriteRepository.SaveAsync();
            await _completedSupplierOrderWriteRepository.SaveAsync();
        }

        public async Task<List<ListSupplierOrder>> GetIncompleteSupplierOrderBySupplierIdAsync(List<Guid> supplierIds)
        {
            var supplierOrders = await _supplierOrderReadRepository.Table
                .Include(x => x.SupplierOrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => supplierIds.Contains(x.SupplierId) && x.IsCompleted) // supplierIds listesindeki değerler ve IsCompleted true olanlar için filtrele
                .ToListAsync();


            if (!supplierOrders.Any())
            {
                return new (); // Boş sonuçlar için null döndür
            }

            return supplierOrders.Select(so => new ListSupplierOrder
            {
                Id = so.Id,
                Address = so.Address,
                Description = so.Description,
                OrderTrackingNumber = Guid.Parse(so.OrderTrackingNumber),
                SupplierId = so.SupplierId,
                IsCompleted = so.IsCompleted,
                TotalPrice = so.TotalPrice,
                ListSupplierOrderItems = so.SupplierOrderItems.Select(soi => new ListSupplierOrderItem
                {
                    CreatedDate = soi.CreatedDate,
                    Price = soi.Price,
                    ProductName = soi.Product.Name,
                    Quantity = soi.Quantity,
                    ProductId = soi.ProductId
                }).ToList(),
            }).ToList();
        }
        public async Task<List<ListSupplierOrder>> GetDecompleteSupplierOrdersBySupplierIdAsync(List<Guid> supplierIds)
        {
            var supplierOrders = await _supplierOrderReadRepository.Table
                .Include(x => x.SupplierOrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => supplierIds.Contains(x.SupplierId) && !x.IsCompleted) 
                .ToListAsync();

            if (!supplierOrders.Any())
            {
                return new();
            }

            return supplierOrders.Select(so => new ListSupplierOrder
            {
                Id = so.Id,
                Address = so.Address,
                Description = so.Description,
                OrderTrackingNumber = Guid.Parse(so.OrderTrackingNumber),
                SupplierId = so.SupplierId,
                IsCompleted = so.IsCompleted,
                TotalPrice = so.TotalPrice,
                ListSupplierOrderItems = so.SupplierOrderItems.Select(soi => new ListSupplierOrderItem
                {
                    CreatedDate = soi.CreatedDate,
                    Price = soi.Price,
                    ProductName = soi.Product.Name,
                    Quantity = soi.Quantity,
                    ProductId = soi.ProductId
                }).ToList(),
            }).ToList();
        }



    }
}
