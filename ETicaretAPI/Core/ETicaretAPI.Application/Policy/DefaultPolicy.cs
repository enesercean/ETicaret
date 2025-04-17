using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Policy
{
    public static class DefaultPolicy
    {
        public const string AdminPolicy = "AdminPolicy";
        public const string CustomerPolicy = "CustomerPolicy";
        public const string SupplierManagerPolicy = "SupplierManagerPolicy";
        public const string EmployeePolicy = "EmployeePolicy";
        public const string EmployeeReadPolicy = "EmployeeReadPolicy";
    }
}
