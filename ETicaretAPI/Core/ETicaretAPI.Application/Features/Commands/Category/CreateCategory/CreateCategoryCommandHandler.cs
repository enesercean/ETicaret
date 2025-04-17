using ETicaretAPI.Application.Abstractions.Services.Authentications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Category.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                    return new CreateCategoryCommandResponse
                    {
                        Success = false,
                        Message = "Category name is required"
                    };

                var category = await _categoryService.AddCategoryAsync(request.Name, request.ParentCategoryId);

                return new CreateCategoryCommandResponse
                {
                    Success = true,
                    Message = "Category created successfully"
                };
            }
            catch (Exception ex)
            {
                return new CreateCategoryCommandResponse
                {
                    Success = false,
                    Message = $"Error creating category: {ex.Message}"
                };
            }
        }
    }
}
