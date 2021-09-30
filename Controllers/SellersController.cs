using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using SalesManager.Models;
using SalesManager.Models.ViewModels;
using SalesManager.Services;

namespace SalesManager.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //Busco todo os vendedores e passo a lista de retorno direto para a View utilizando o IActionResult
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            //Chama a view Create e passo uma lista com todos os departamentos para a View
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previne o ataque de sessão
        public IActionResult Create(Seller seller) //O nome do parâmetro deve ser exatamente o nome da classe que será inserida
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id) //Interrogação significa que o parâmetro é opcinal
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value); //Busca o Seller por ID

            if (obj == null)
            {
                return NotFound();
            }

            //Passa o Seller encontrado para a View
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previne o ataque de sessão
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value); //Busca o Seller por ID

            if (obj == null)
            {
                return NotFound();
            }

            //Passa o Seller encontrado para a View
            return View(obj);
        }
    }
}