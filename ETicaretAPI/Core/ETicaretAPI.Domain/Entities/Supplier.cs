using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<SupplierAddress> SupplierAddresses { get; set; }
        public ICollection<SupplierContactPerson> SupplierContactPeople { get; set; }
        public ICollection<Product> Products { get; set; }
    }

}
