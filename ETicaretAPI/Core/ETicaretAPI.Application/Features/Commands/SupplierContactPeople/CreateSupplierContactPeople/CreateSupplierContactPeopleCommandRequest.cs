using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.CreateSupplierContactPeople
{
    public class CreateSupplierContactPeopleCommandRequest : IRequest<CreateSupplierContactPeopleCommandResponse>
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
