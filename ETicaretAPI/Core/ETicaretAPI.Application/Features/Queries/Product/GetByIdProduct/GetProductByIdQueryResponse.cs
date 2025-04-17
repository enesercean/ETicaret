using ETicaretAPI.Application.DTOs.ProductRating;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryResponse
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }



        public string? BrandName { get; set; }
        public string? SupplierName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }

        public List<ListProductRating>? Ratings { get; set; }
    }
}
