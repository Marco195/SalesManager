using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesManager.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ficar entre {2} e {1}")] //0 é o nome, 1 é o maximo e 2 é o minimo
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] //Formata para apenas 2 casas decimais
        public DateTime BirthDate { get; set; }

        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser de {1} a {2}")]
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] //Formata para apenas 2 casas decimais
        public double Basesalary { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Department Id")] //Seta o nome que será exibido na interface da aplicação
        [DataType(DataType.Date)] //Seta o tipo que será exibido apenas para Data, sem hora
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double basesalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Basesalary = basesalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        /// <summary>
        /// Calculate total sales.
        /// </summary>
        /// <param name="initial">Initial Date</param>
        /// <param name="final">Final Date</param>
        /// <returns>Total Sales</returns>
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
