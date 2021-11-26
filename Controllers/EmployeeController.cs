using CRUD_PostgreSQL.DTO;
using CRUD_PostgreSQL.Helper;
using CRUD_PostgreSQL.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _IRepository;

        public EmployeeController(IEmployeeRepository IRepository)
        {
            _IRepository = IRepository;
        }


        [HttpPost]
        [Route("CreateEmployee")]

        public async Task<MessageHelper> CreateEmployee(EmployeeDTO create)
        {
            var msg = await _IRepository.CreateEmployee(create);

            return msg;
        }

        [HttpGet]
        [Route("GetEmployee")]

        public async Task<IActionResult> GetEmployee()
        {
            var dt = await _IRepository.GetEmployee();

            return Ok(dt);
        }


    }
}
