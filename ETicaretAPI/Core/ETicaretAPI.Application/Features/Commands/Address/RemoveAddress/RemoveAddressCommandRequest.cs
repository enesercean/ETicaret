﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Address.RemoveAddress
{
    public class RemoveAddressCommandRequest : IRequest<RemoveAddressCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
