using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.ViewModels.Products
{
    public class VM_Create_Product
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
    }
}
