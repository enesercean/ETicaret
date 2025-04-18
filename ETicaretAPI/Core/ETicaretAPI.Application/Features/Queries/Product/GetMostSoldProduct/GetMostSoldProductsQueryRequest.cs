﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetMostSoldProduct
{
    public class GetMostSoldProductsQueryRequest : IRequest<GetMostSoldProductsQueryResponse>
    {
        public int Count { get; set; } = 10;
    }
}
