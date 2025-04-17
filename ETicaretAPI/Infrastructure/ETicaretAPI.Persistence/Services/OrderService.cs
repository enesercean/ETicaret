using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.BasketItem;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.CompletedOrderRepositories;
using ETicaretAPI.Domain.Entities;
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
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            if (!Guid.TryParse(createOrder.BasketId, out Guid basketId))
            {
                throw new ArgumentException("Invalid BasketId");
            }

            await _orderWriteRepository.AddAsync(new()
            {
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                Address = createOrder.Address,
                OrderTrackingNumber = Guid.NewGuid().ToString()
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<List<ListOrder>> GetAllOrderAsync()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var table = _orderReadRepository.Table
                .Include(o => o.Basket)
                    .ThenInclude(o => o.User)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)
                        .ThenInclude(bi => bi.Product)
                            .ThenInclude(p => p.Supplier)
                                .ThenInclude(s => s.SupplierContactPeople)
                .Where(x => x.CompletedOrder == null)
                .Select(o => new ListOrder
                {
                    Id = o.Id.ToString(),
                    CreatedDate = o.CreatedDate,
                    OrderTrackingCode = o.OrderTrackingNumber,
                    TotalPrice = o.Basket.BasketItems
                        .Where(bi => bi.Product.Supplier.SupplierContactPeople
                            .Any(scp => scp.UserId == userId))
                        .Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    BasketItems = o.Basket.BasketItems
                        .Where(bi => bi.Product.Supplier.SupplierContactPeople
                            .Any(scp => scp.UserId == userId))
                        .Select(bi => new ListBasketItem
                        {
                            ProductId = bi.Product.Id.ToString(),
                            ProductName = bi.Product.Name,
                            Quantity = bi.Quantity,
                            Price = bi.Product.Price,
                            CreatedDate = bi.CreatedDate
                        }).ToList()
                })
                .Where(o => o.BasketItems.Any())
                .ToList();

            return table;
        }

        public async Task CreateCompletedOrderAsync(string completedOrderId, string orderTrackingNumber)
        {
            var order = await _orderReadRepository.Table
                .Include(o => o.SupplierOrders)
                .FirstOrDefaultAsync(o => o.SupplierOrders.Any(so => so.Id == Guid.Parse(completedOrderId)));

            if (order == null)
            {
                return;
            }

            var allSupplierOrdersCompleted = order.SupplierOrders
                .All(so => so.IsCompleted);

            if (!allSupplierOrdersCompleted)
            {
                return;
            }

            await _completedOrderWriteRepository.AddAsync(new()
            {
                Id = order.Id,
                OrderTrackingNumber = order.OrderTrackingNumber
            });
            await _completedOrderWriteRepository.SaveAsync();
        }



        public async Task<List<ListOrder>> GetAllCompletedOrderAsync() 
        {
            var table = _orderWriteRepository.Table.Include(o => o.Basket)
                .ThenInclude(o => o.User)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .Where(o => o.CompletedOrder != null)
                .Select(o => new ListOrder
                {
                    Id = o.Id.ToString(),
                    CreatedDate = o.CreatedDate,
                    OrderTrackingCode = o.OrderTrackingNumber,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    BasketItems = o.Basket.BasketItems.Select(bi => new ListBasketItem
                    {
                        ProductId = bi.Product.Id.ToString(),
                        ProductName = bi.Product.Name,
                        Quantity = bi.Quantity,
                        Price = bi.Product.Price,
                        CreatedDate = bi.CreatedDate
                    }).ToList()
                })
                .ToList();
            return table;
        }

        public async Task<List<ListOrder>> GetOrdersByUserAsync(string userId)
        {
            var orders = await _orderReadRepository.Table
                .Include(o => o.Basket)
                    .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                    .ThenInclude(b => b.BasketItems)
                        .ThenInclude(bi => bi.Product)
                .Where(o => o.Basket.UserId == userId)
                .Select(o => new ListOrder
                {
                    Id = o.Id.ToString(),
                    CreatedDate = o.CreatedDate,
                    OrderTrackingCode = o.OrderTrackingNumber,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    BasketItems = o.Basket.BasketItems.Select(bi => new ListBasketItem
                    {
                        ProductId = bi.Product.Id.ToString(),
                        ProductName = bi.Product.Name,
                        Quantity = bi.Quantity,
                        Price = bi.Product.Price,
                        CreatedDate = bi.CreatedDate
                    }).ToList()
                })
                .ToListAsync();

            return orders;
        }

    }
}
