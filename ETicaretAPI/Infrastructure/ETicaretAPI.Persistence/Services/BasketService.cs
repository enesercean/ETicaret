using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Application.ViewModels.Baskets;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketReadRepository _basketReadRepository;
        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }

        private async Task<Basket> GetUsernameBasket()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            var user = await _userManager.Users
                .Include(u => u.Baskets)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var targetBasket = user?.Baskets?
                .GroupJoin(
                    _orderReadRepository.Table,
                    basket => basket.Id,
                    order => order.Id,
                    (basket, orders) => new { Basket = basket, Order = orders.FirstOrDefault() }
                )
                .FirstOrDefault(b => b.Order == null)?.Basket;

            if (targetBasket == null)
            {
                targetBasket = new Basket();
                user?.Baskets?.Add(targetBasket);
                await _basketWriteRepository.SaveAsync();
            }
            else
            {
                targetBasket = _basketReadRepository.Table
                    .Include(b => b.BasketItems)
                        .ThenInclude(bi => bi.Product) // Product'ları da dahil et
                    .FirstOrDefault(b => b.Id == targetBasket.Id);
            }

                return targetBasket;
        }

        public async Task AddItemToBasketAsync(VMCreateBasketItem basketItem)
        {
            Basket? basket = await GetUsernameBasket();
            if (basket != null)
            {
                //BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId.ToString() == basketItem.ProductId);
                BasketItem? _basketItem = await _basketItemReadRepository.GetWhere(bi => bi.BasketId == basket.Id && bi.ProductId.ToString() == basketItem.ProductId).FirstOrDefaultAsync();
                if (_basketItem != null)
                {
                    _basketItem.Quantity++;
                }
                else
                {
                    _basketItem = new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity
                    };
                    await _basketItemWriteRepository.AddAsync(_basketItem);
                }
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket basket = await GetUsernameBasket();
            Basket? result = await _basketReadRepository.Table
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveItemFromBasketAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(VMUpdateBasketItem basketItem)
        {
            BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public Basket? GetUserActiveBasket
        {
            get
            {
                Basket? basket = GetUsernameBasket().Result;
                return basket;
            }
        }
    }
}
