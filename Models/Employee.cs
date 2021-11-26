using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.Models
{
    public class EmployeeBasicInfo
    {
        [Key]
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long DepartmenId { get; set; }
        public DateTime DateofJoining { get; set; }
        public decimal BasicSalary { get; set; }
    }
}
