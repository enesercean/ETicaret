using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.UpdateUserRole
{
    public class UpdateUserRoleCommandRequest : IRequest<UpdateUserRoleCommandResponse>
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
    }
}
