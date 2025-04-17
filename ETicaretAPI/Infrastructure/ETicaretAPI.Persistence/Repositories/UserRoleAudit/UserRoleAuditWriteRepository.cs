using ETicaretAPI.Application.Repositories.UserRoleAudit;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.UserRoleAudit
{
    public class UserRoleAuditWriteRepository : WriteRepository<Domain.Entities.UserRoleAudit>, IUserRoleAuditWriteRepository
    {
        public UserRoleAuditWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
