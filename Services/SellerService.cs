using Microsoft.EntityFrameworkCore;
using SalesManager.Data;
using SalesManager.Models;
using SalesManager.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Services
{
    public class SellerService
    {
        private readonly SalesManagerContext _context;

        /*O obj do tipo "Context" é reponsável por lidar com a interação com o banco de dados através do C# */
        public SellerService(SalesManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Find all Sellers in DB.
        /// </summary>
        /// <returns>List of Seller</returns>
        //public List<Seller> FindAll()
        //{
        //    //Vai na tabela "Seller" e retorna todos os registro do BD
        //    return _context.Seller.ToList();
        //}

        /// <summary>
        /// Find all Sellers in DB.
        /// </summary>
        /// <returns>List of Seller</returns>
        public async Task<List<Seller>> FindAllAsync()
        {
            //Vai na tabela "Seller" e retorna todos os registro do BD
            return await _context.Seller.ToListAsync();
        }

        /// <summary>
        /// Insert a new Seller on DB.
        /// </summary>
        /// <param name="obj"></param>
        //public void Insert(Seller obj)
        //{
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Find Seller by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public Seller FindById(int id)
        //{
        //    //também retorna o Department atravésd o Include. O entity realiza o Join das tabelas através do include
        //    return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        //}

        public async Task<Seller> FindByIdAsync(int id)
        {
            //também retorna o Department atravésd o Include. O entity realiza o Join das tabelas através do include
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        /// <summary>
        /// Delete Seller from DB.
        /// </summary>
        /// <param name="id"></param>
        //public void Remove(int id)
        //{
        //    var obj = _context.Seller.Find(id);
        //    _context.Seller.Remove(obj);
        //    _context.SaveChanges();
        //}

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        //public void Update(Seller seller)
        //{
        //    if (!_context.Seller.Any(x => x.Id == seller.Id))
        //    {
        //        throw new NotFoundException("Id not found.");
        //    }

        //    try
        //    {
        //        _context.Update(seller);
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        throw new DbConcurrencyException(ex.Message);
        //    }
        //}

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found.");
            }

            try
            {
                _context.Update(seller);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
