using AppLanchesWeb.Repository.Interface;
using AppLanchesWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLanchesWeb.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository    _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            //var lanches = _lancheRepository.lanches;
            //return View(lanches);
            var lancheListViewModel = new LancheListViewModel();

            lancheListViewModel.Lanches = _lancheRepository.lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual";
            return View(lancheListViewModel);
        }
    }
}
