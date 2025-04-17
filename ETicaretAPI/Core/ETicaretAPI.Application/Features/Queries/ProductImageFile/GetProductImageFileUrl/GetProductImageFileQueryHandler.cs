using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductImageFileUrl
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, GetProductImageFileQueryResponse>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GetProductImageFileQueryHandler(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<GetProductImageFileQueryResponse> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "photo-images", request.FileName);

            if (!File.Exists(imagePath))
            {
                return null;
            }

            var imageBytes = await File.ReadAllBytesAsync(imagePath, cancellationToken);
            return new GetProductImageFileQueryResponse
            {
                ImageBytes = imageBytes,
                ContentType = "image/png"
            };
        }
    }
}
