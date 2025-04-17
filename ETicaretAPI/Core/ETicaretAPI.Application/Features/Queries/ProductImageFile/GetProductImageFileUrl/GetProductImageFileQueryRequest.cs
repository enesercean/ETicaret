using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductImageFileUrl
{
    public class GetProductImageFileQueryRequest : IRequest<GetProductImageFileQueryResponse>
    {
        public string Id { get; set; }
        public string FileName { get; set; }
    }
}
