using System;
using System.ComponentModel.DataAnnotations;
using SalesManager.Models.Enum;

namespace SalesManager.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }
        /// <summary>
        /// Salve status Pending = 0, Billed = 1, Canceled = 2
        /// </summary>
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
