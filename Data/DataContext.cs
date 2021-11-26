using CRUD_PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EmployeeBasicInfo> EmployeeBasicInfo { get; set; }

        //public Task<int> SaveChangeAsync(CancellationToken cancelationToken = default);
    }
}
