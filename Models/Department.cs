using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesManager.Models
{
    public class Department
    {
        /// <summary>
        /// Department's Id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Department's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of Sellers.
        /// </summary>
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
