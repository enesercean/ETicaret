﻿using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
    }
}
