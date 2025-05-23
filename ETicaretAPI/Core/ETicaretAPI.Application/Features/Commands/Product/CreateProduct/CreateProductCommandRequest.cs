﻿using ETicaretAPI.Application.Features.Commands.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public List<Guid>? CategoryIds { get; set; }
        public Guid? BrandId { get; set; }
    }
}
