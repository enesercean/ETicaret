﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.SupplierRegistrationRequest.GetSupplierRegistrationRequests
{
    public class GetSupplierRegistrationRequestsQueryRequest : IRequest<List<GetSupplierRegistrationRequestsQueryResponse>>
    {
    }
}
