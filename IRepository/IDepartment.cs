using CRUD_PostgreSQL.DTO;
using CRUD_PostgreSQL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.IRepository
{
    public interface IDepartment
    {
        public Task<DepartmentDTO> GetById (long departmentId);
        public Task<List<DepartmentDTO>> GetAll ();
        public Task<MessageHelper> CreateDepartment(DepartmentDTO create);
        public Task<MessageHelper> Update (DepartmentDTO update);

    }
}
