using SalesManager.Data;
using SalesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Services
{
    public class SellerService
    {
        private readonly SalesManagerContext _context;

        /*O obj do tipo "Context" é reposnsável por lidar com a insteração com o banco de dados através do C# */

        public SellerService(SalesManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Find all Sellers in DB.
        /// </summary>
        /// <returns>List of Seller</returns>
        public List<Seller> FindAll()
        {
            //Vai na tabela "Seller" e retorna todos os registro do BD
            return _context.Seller.ToList();
        }

        /// <summary>
        /// Insert a new Seller on DB.
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
