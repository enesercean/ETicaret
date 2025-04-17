using E=ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories.Category
{
    public interface ICategoryWriteRepository : IWriteRepository<E.Category>
    {
    }
}
