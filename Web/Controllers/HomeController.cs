using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortFolioItem> _portfolio;

        public HomeController(IUnitOfWork<Owner>owner,IUnitOfWork<PortFolioItem>portfolio)
        {
            _owner = owner;
            _portfolio = portfolio;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                portFolioItems = _portfolio.Entity.GetAll().ToList()
            };

            return View(homeViewModel);
        }
    }
}
