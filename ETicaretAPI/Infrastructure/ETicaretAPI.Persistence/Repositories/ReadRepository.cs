using ETicaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Application.Repositories.CustomerRepositories;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            //return Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            //return await Table.FindAsync(Guid.Parse(id));
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data=> data.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
    }
}
