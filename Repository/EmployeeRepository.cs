using CRUD_PostgreSQL.Data;
using CRUD_PostgreSQL.DTO;
using CRUD_PostgreSQL.Helper;
using CRUD_PostgreSQL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreateEmployee(EmployeeDTO create)
        {
            try
            {
                var data = new Models.EmployeeBasicInfo
                {
                    EmployeeId = create.EmployeeId,
                    EmployeeName = create.EmployeeName,
                    DepartmenId = create.DepartmentId,
                    DateofJoining = create.DateofJoining,
                    BasicSalary = create.BasicSalary
                };

                await _context.EmployeeBasicInfo.AddAsync(data);
                await _context.SaveChangesAsync();

                var msg = new MessageHelper
                {
                    Message = "Create Successfully",
                    statusCode = 200

                };

                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeDTO>> GetEmployee()
        {
            try
            {
                var data = await Task.FromResult((from a in _context.EmployeeBasicInfo
                                                  join b in _context.Department on a.DepartmenId equals b.DepartmentId
                                                  select new EmployeeDTO
                                                  {
                                                      EmployeeId = a.EmployeeId,
                                                      EmployeeName = a.EmployeeName,
                                                      DepartmentId = a.DepartmenId,
                                                      DepartmentName = b.DepartmentName,
                                                      BasicSalary = a.BasicSalary,
                                                      DateofJoining = a.DateofJoining
                                                  }).ToList());
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
