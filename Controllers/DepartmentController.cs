
using CRUD_PostgreSQL.DTO;
using CRUD_PostgreSQL.Helper;
using CRUD_PostgreSQL.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _IRepository;

        public DepartmentController(IDepartment IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpPost]
        [Route("CreateDepartment")]
     
        public async Task<MessageHelper> CreateDepartment(DepartmentDTO objCreate)
        {
            var msg = await _IRepository.CreateDepartment(objCreate);

            return msg;
        }

        [HttpGet]
        [Route("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var dt = await _IRepository.GetAll();
   
            return Ok(dt);
        }

        [HttpGet]
        [Route("GetById")]

        public async Task<IActionResult> GetById(long departmentId)
        {
            var dt = await _IRepository.GetById(departmentId);

            return Ok(dt);
        }


        [HttpPut]
        [Route("Update")]

        public async Task<MessageHelper> Update(DepartmentDTO update)
        {
            var msg = await _IRepository.Update(update);
            return msg;
        }
    }
}
