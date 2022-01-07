using Microsoft.EntityFrameworkCore;
using SalesManager.Data;
using SalesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Services
{
    public class DepartmentService
    {
        private readonly SalesManagerContext _context;

        /*O obj do tipo "Context" é reposnsável por lidar com a insteração com o banco de dados através do C# */

        public DepartmentService(SalesManagerContext context)
        {
            _context = context;
        }

        //public List<Department> FindAll()
        //{
        //    return _context.Department.OrderBy(x => x.Name).ToList();
        //}

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
