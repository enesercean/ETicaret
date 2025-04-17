using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Supplier.GetSupplierByUser
{
    public class GetSupplierByUserQueryResponse
    {
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
    }
}
