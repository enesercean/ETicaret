// Application/ETicaretAPI.Application/Features/Queries/ProductLike/GetUserLikedProducts/GetUserLikedProductsQueryResponse.cs
using ETicaretAPI.Application.DTOs.ProductLike;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.GetUserLikedProducts
{
    public class GetUserLikedProductsQueryResponse
    {
        public List<MostLikedProduct> Products { get; set; }
    }

}
