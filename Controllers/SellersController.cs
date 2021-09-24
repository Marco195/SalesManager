using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using SalesManager.Models;
using SalesManager.Services;

namespace SalesManager.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //Busco todo os vendedores e passo a lista de retorno direto para a View utilizando o IActionResult
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            //Chama a view Create
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previne a que aplicação sofre ataques durante a inserção de dados no banco
        public IActionResult Create(Seller obj)
        {
            _sellerService.Insert(obj);
            return RedirectToAction(nameof(Index));
        }
    }
}