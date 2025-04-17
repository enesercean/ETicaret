using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Brand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Brand.GetBrandByCategory
{
    public class GetBrandByCategoryQueryHandler : IRequestHandler<GetBrandByCategoryQueryRequest, List<GetBrandByCategoryQueryResponse>>
    {
        readonly IBrandService _brandService;

        public GetBrandByCategoryQueryHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<List<GetBrandByCategoryQueryResponse>> Handle(GetBrandByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetBrandByCategories(request.CategoryIdList);

            var result = brands.Select(b => new GetBrandByCategoryQueryResponse
            {
                BrandId = b.BrandId,
                Name = b.Name
            }).ToList();
            return result;
        }
    }
}
