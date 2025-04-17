using ETicaretAPI.Application.Abstractions.Services.Authentications;
using ETicaretAPI.Application.Repositories.ProductCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductCategory.GetAllProductCategory
{
    public class GetAllProductCategoryHandler : IRequestHandler<GetAllProductCategoryRequest, GetAllProductCategoryResponse>
    {
        readonly ICategoryService _categoryService;

        public GetAllProductCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetAllProductCategoryResponse> Handle(GetAllProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllCategories();
            return new()
            {
                Categories = categories
            };
        }
    }
}
