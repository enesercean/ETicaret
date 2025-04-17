using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.GetSupplierContactPeopleByUser
{
    public class GetSupplierContactPeopleByUserQueryResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string AssignedDate { get; set; }
    }
}
