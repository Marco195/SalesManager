using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
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
    }
}