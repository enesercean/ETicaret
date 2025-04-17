using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        private readonly IBrandService _brandService;

        public CreateBrandCommandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new CreateBrandCommandResponse
                    {
                        Success = false,
                        Message = "Brand name is required"
                    };
                }

                var brandId = await _brandService.CreateBrandAsync(request.Name, request.CategoryIds);

                return new CreateBrandCommandResponse
                {
                    Success = true,
                    Message = "Brand created successfully",
                };
            }
            catch (Exception ex)
            {
                return new CreateBrandCommandResponse
                {
                    Success = false,
                    Message = $"Error creating brand: {ex.Message}"
                };
            }
        }
    }
}
