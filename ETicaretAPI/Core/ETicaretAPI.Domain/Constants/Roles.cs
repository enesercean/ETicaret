using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string SupplierManager = "SupplierManager";
        public const string Employee = "Employee";
        public const string EmployeeRead = "EmployeeRead";
        public const string Customer = "Customer";

        public static readonly IReadOnlyList<string> AllRoles = new List<string>
        {
            Admin,
            SupplierManager,
            Employee,
            EmployeeRead,
            Customer
        };
    }
}
