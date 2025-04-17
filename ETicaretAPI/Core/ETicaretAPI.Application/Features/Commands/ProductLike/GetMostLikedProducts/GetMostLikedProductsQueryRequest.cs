// Application/ETicaretAPI.Application/Features/Queries/ProductLike/GetMostLikedProducts/GetMostLikedProductsQueryRequest.cs
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.GetMostLikedProducts
{
    public class GetMostLikedProductsQueryRequest : IRequest<GetMostLikedProductsQueryResponse>
    {
        public int Count { get; set; } = 10;
    }
}
