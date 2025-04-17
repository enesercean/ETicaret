using MediatR;
using System;
using System.Collections.Generic;

namespace ETicaretAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequest : IRequest<CreateBrandCommandResponse>
    {
        public string Name { get; set; }
        public List<Guid>? CategoryIds { get; set; }
    }
}
