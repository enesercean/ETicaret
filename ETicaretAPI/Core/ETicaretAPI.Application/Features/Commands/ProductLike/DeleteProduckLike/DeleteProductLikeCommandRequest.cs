using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductLike.DeleteProduckLike
{
    public class DeleteProductLikeCommandRequest : IRequest<DeleteProductLikeCommandResponse>
    {
        public Guid ProductId { get; set; }
    }
}
