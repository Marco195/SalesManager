using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesManager.Models;

namespace SalesManager.Data
{
    public class SalesManagerContext : DbContext
    {
        public SalesManagerContext (DbContextOptions<SalesManagerContext> options)
            : base(options)
        {
        }

        public DbSet<SalesManager.Models.Department> Department { get; set; }
    }
}
