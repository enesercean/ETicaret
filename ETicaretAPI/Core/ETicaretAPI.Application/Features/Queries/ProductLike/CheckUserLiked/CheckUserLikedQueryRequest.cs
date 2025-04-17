// Application/ETicaretAPI.Application/Features/Queries/ProductLike/CheckUserLiked/CheckUserLikedQueryRequest.cs
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.CheckUserLiked
{
    public class CheckUserLikedQueryRequest : IRequest<CheckUserLikedQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
