using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductImage
{
    public class GetProductImageQueryRequest : IRequest<GetProductImageQueryResponse>
    {
        public string Id { get; set; }
    }
}
