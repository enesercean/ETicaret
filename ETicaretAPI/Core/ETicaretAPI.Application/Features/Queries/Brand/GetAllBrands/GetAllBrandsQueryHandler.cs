using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Brand.GetAllBrands
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQueryRequest, List<GetAllBrandsQueryResponse>>
    {
        private readonly IBrandService _brandService;

        public GetAllBrandsQueryHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<List<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return brands.Select(b => new GetAllBrandsQueryResponse
            {
                BrandId = b.BrandId,
                Name = b.Name,
                Categories = b.Categories
            }).ToList();
        }
    }
}
