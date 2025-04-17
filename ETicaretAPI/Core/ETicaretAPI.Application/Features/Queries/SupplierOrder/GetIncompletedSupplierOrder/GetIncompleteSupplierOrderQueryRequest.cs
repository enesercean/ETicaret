using MediatR;
namespace ETicaretAPI.Application.Features.Queries.SupplierOrder.GetIncompletedSupplierOrder
{
    public class GetIncompleteSupplierOrderQueryRequest : IRequest<List<GetIncompleteSupplierOrderQueryResponse>>
    {
        public Guid UserId { get; set; }
    }
}
   