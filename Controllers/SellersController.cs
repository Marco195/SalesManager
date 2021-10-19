using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using SalesManager.Models;
using SalesManager.Models.ViewModels;
using SalesManager.Services;
using SalesManager.Services.Exceptions;

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

        public async Task<IActionResult> Index()
        {
            //Busco todo os vendedores e passo a lista de retorno direto para a View utilizando o IActionResult
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments =await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            //Chama a view Create e passo uma lista com todos os departamentos para a View
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previne o ataque de sessão
        public async Task<IActionResult> Create(Seller seller) //O nome do parâmetro deve ser exatamente o nome da classe que será inserida
        {
            if (!ModelState.IsValid)//Se o seller não for válido pede para o usuário preencher o cadastro novamente
            {
                var departaments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departaments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) //Interrogação significa que o parâmetro é opcinal
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided."});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); //Busca o Seller por ID

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            //Passa o Seller encontrado para a View
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previne o ataque de sessão
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); //Busca o Seller por ID

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            //Passa o Seller encontrado para a View
            return View(obj);
        }

        public async Task<IActionResult >Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); //Busca o Seller por ID
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found." });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previne o ataque de sessão
        public async Task<IActionResult> Edit(int? id, Seller seller)
        {
            if (!ModelState.IsValid)//Se o seller não for válido pede para o usuário preencher o cadastro novamente
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch." });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var vieModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(vieModel);
        }
    }
}