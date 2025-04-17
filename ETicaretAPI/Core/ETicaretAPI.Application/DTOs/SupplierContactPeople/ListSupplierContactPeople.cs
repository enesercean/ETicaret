using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.SupplierContactPeople
{
    public class ListSupplierContactPeople
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid UserId { get; set; }
    }
}
