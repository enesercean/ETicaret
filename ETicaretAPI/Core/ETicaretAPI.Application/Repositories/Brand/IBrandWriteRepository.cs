﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Repositories.Brand
{
    public interface IBrandWriteRepository : IWriteRepository<E.Brand>
    {
    }
}
