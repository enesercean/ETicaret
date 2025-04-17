using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public AppRole()
        {
            Id = Guid.NewGuid().ToString(); // Id'ye otomatik GUID değeri atanıyor
        }

        public AppRole(string roleName) : this()
        {
            Name = roleName;
        }
    }
}
