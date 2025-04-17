using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductBySupplier
{
    public class GetProductBySupplierQueryHandler : IRequestHandler<GetProductBySupplierQueryRequest, GetProductBySupplierQueryResponse>
    {
        readonly IProductService _productService;
        readonly IUserService _userService;
        readonly ISupplierService _supplierService;

        public GetProductBySupplierQueryHandler(IProductService productService, IUserService userService, ISupplierService supplierService)
        {
            _productService = productService;
            _userService = userService;
            _supplierService = supplierService;
        }

        public async Task<GetProductBySupplierQueryResponse> Handle(GetProductBySupplierQueryRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();

            var supplier = await _supplierService.GetSupplierAsync(userId);

            var products = await _productService.GetAllProductBySupplierAsync(supplier.SupplierId.ToString());

            return new()
            {
                Products = products
            };
        }
    }
}
