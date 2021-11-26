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
    public class DepartmentRepository : IDepartment
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreateDepartment(DepartmentDTO create)
        {
            try
            {
                var data = new Models.Department
                {
                    //DepartmentId = create.DepartmentId,
                    DepartmentName = create.DepartmentName,

                };

                await _context.Department.AddAsync(data);
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

        public async Task<List<DepartmentDTO>> GetAll()
        {
           try
            {
                var data = await Task.FromResult((from a in _context.Department
                                                  select new DepartmentDTO
                                                  {
                                                      DepartmentId = a.DepartmentId,
                                                      DepartmentName = a.DepartmentName
                                                  }).ToList());

                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DepartmentDTO> GetById(long departmentId)
        {
            try
            {
                var data = await Task.FromResult((from a in _context.Department
                                                  where a.DepartmentId == departmentId
                                                  select new DepartmentDTO
                                                  {
                                                      DepartmentId = a.DepartmentId,
                                                      DepartmentName = a.DepartmentName
                                                  }).FirstOrDefault());
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MessageHelper> Update (DepartmentDTO update)
        {
            try
            {
                var data = _context.Department.Where(x => x.DepartmentId == update.DepartmentId).FirstOrDefault();
                if (data == null)
                    throw new Exception("Edit Data Not Found");

                data.DepartmentName = update.DepartmentName;

                _context.Department.Update(data);
                await _context.SaveChangesAsync();

                var msg = new MessageHelper
                {
                    Message = "Update Successfully",
                    statusCode = 200
                };

                return msg;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
