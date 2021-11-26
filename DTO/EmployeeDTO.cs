using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.DTO
{
    public class EmployeeDTO
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateofJoining { get; set; }
        public decimal BasicSalary { get; set; }
    }
}
