using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.UserRoleAudit
{
    public class ListUserRoleAudit
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
