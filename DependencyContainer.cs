using CRUD_PostgreSQL.IRepository;
using CRUD_PostgreSQL.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IDepartment, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }

    }
}
