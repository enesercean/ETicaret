using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
    {
        readonly IBasketService _basketService;

        public RemoveBasketItemCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        async Task<RemoveBasketItemCommandResponse> IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>.Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.RemoveItemFromBasketAsync(request.BasketItemId);
            return new();
        }
    }
}
