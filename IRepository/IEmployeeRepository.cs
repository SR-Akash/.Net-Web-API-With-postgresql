using CRUD_PostgreSQL.DTO;
using CRUD_PostgreSQL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.IRepository
{
    public interface IEmployeeRepository
    {
        public Task<MessageHelper> CreateEmployee(EmployeeDTO create);
        public Task<List<EmployeeDTO>> GetEmployee ();
    }
}
