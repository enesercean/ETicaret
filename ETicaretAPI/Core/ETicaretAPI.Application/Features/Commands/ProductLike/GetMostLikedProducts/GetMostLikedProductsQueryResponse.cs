using ETicaretAPI.Application.DTOs.ProductLike;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.GetMostLikedProducts
{
    public class GetMostLikedProductsQueryResponse
    {
        public List<MostLikedProduct> Products { get; set; }
    }
}
