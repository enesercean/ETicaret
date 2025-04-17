using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Brand.GetAllBrands
{
    public class GetAllBrandsQueryRequest : IRequest<List<GetAllBrandsQueryResponse>>
    {
    }
}
