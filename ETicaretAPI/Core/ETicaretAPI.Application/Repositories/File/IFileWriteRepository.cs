﻿using ETicaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Repositories
{
    public interface IFileWriteRepository : IWriteRepository<F.File>
    {
    }
}
